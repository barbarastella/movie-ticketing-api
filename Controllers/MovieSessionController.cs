using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Services.MovieSessions;

namespace MovieTicketingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieSessionsController : ControllerBase
{
    private readonly IMovieSessionService _movieSessionService;
    public record CreateMovieSessionDto(Guid MovieId, Guid RoomId, DateTime StartTime);

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
    public async Task<IActionResult> Create([FromBody] CreateMovieSessionDto dto)
    {
        var session = await _movieSessionService.CreateSessionAsync(dto.MovieId, dto.RoomId, dto.StartTime);
        if (session == null) return BadRequest(new { message = "Filme ou Sala informados não existem no banco de dados." });

        return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _movieSessionService.DeleteAsync(id);
        return NoContent();
    }
}