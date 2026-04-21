using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Services.MovieSessions;

namespace MovieTicketingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieSessionsController : ControllerBase
{
    private readonly IMovieSessionService _movieSessionService;

    public MovieSessionsController(IMovieSessionService movieSessionService)
    {
        _movieSessionService = movieSessionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sessions = await _movieSessionService.GetAllAsync();
        return Ok(sessions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var session = await _movieSessionService.GetByIdAsync(id);
        if (session == null) return NotFound(new { message = "Sessão não encontrada." });

        return Ok(session);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateMovieSessionDto dto)
    {
        var session = await _movieSessionService.CreateAsync(dto);
        if (session == null) return BadRequest(new { message = "Filme ou Sala informados não existem." });

        return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieSessionDto dto)
    {
        try
        {
            var updatedSession = await _movieSessionService.UpdateAsync(id, dto);
            if (updatedSession == null) return NotFound(new { message = "Sessão não encontrada." });

            return Ok(updatedSession);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _movieSessionService.DeleteAsync(id);
        return NoContent();
    }
}