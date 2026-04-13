using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Services;

namespace MovieTicketingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = await _service.GetMoviesAsync();

        if (movies == null) return NotFound();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetById(Guid id)
    {
        var movie = await _service.GetMovieByIdAsync(id);

        if (movie == null) return NotFound(new { message = "Filme não encontrado." });

        return Ok(movie);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<Movie>> Create(Movie movie)
    {
        await _service.CreateMovieAsync(movie);
        return CreatedAtAction(nameof(GetAll), new { id = movie.Id }, movie);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, Movie movie)
    {
        if (id != movie.Id) return BadRequest(new { message = "O ID da URL não coincide com o ID do corpo da requisição." });

        try
        {
            await _service.UpdateMovieAsync(id, movie);
            return NoContent();
        } catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingMovie = await _service.GetMovieByIdAsync(id);

        if (existingMovie == null) return NotFound(new { message = "Filme não encontrado para exclusão." });

        await _service.DeleteMovieAsync(id);
        return NoContent();
    }
}
