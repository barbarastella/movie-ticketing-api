using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Services;
using NpgsqlTypes;

namespace MovieTicketingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;

    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllAsync()
    {
        var movies = await _service.GetAllAsync();

        if (movies == null) return NotFound();

        return Ok(movies);
    }

    [HttpGet("{id}", Name = "GetMovieById")]
    [AllowAnonymous]
    public async Task<ActionResult<Movie>> GetByIdAsync(Guid id)
    {
        var movie = await _service.GetByIdAsync(id);

        if (movie == null) return NotFound(new { message = "Filme não encontrado." });

        return Ok(movie);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Movie>> CreateAsync([FromBody] CreateMovieDto dto)
    {
        var movie = await _service.CreateAsync(dto);
        return CreatedAtRoute("GetMovieById", new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Movie movie)
    {
        if (id != movie.Id) return BadRequest(new { message = "O ID da URL não coincide com o ID do corpo da requisição." });

        try
        {
            await _service.UpdateAsync(id, movie);
            return NoContent();
        } catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var existingMovie = await _service.GetByIdAsync(id);

        if (existingMovie == null) return NotFound(new { message = "Filme não encontrado para exclusão." });

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
