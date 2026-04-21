using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Services;
using MovieTicketingAPI.Services.Rooms;

namespace MovieTicketingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    public record CreateRoomDto(string Name);

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomService.GetAllAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room == null) return NotFound(new { message = "Sala não encontrada." });

        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest(new { message = "O nome da sala é obrigatório." });

        var room = await _roomService.CreateRoomAsync(dto.Name);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _roomService.DeleteAsync(id);
        return NoContent();
    }

}
