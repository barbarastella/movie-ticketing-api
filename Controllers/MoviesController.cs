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
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService service)
    {
        _movieService = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = await _movieService.GetAllAsync();
        if (movies == null) return NotFound();

        return Ok(movies);
    }

    [HttpGet("{id}", Name = "GetMovieById")]
    [AllowAnonymous]
    public async Task<ActionResult<Movie>> GetById(Guid id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie == null) return NotFound(new { message = "Filme não encontrado." });

        return Ok(movie);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieDto dto)
    {
        var movie = await _movieService.CreateAsync(dto);
        return CreatedAtRoute("GetMovieById", new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieDto dto)
    {
        var updatedMovie = await _movieService.UpdateAsync(id, dto);
        if (updatedMovie == null) return NotFound(new { message = "Filme não encontrado." });

        return Ok(updatedMovie);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingMovie = await _movieService.GetByIdAsync(id);

        if (existingMovie == null) return NotFound(new { message = "Filme não encontrado para exclusão." });

        await _movieService.DeleteAsync(id);
        return NoContent();
    }
}
