using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Services.Seats;

namespace MovieTicketingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;
    public record CreateSeatDto(string SeatNumber, Guid RoomId);

    public SeatsController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetByRoom(Guid roomId)
    {
        var seats = await _seatService.GetByRoomAsync(roomId);
        return Ok(seats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var seat = await _seatService.GetByIdAsync(id);
        if (seat == null) return NotFound(new { message = "Poltrona não encontrada." });

        return Ok(seat);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeatDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.SeatNumber)) return BadRequest(new { message = "O número da poltrona é obrigatório (ex: A1)." });

        var seat = await _seatService.CreateAsync(dto.SeatNumber, dto.RoomId);
        if (seat == null) return NotFound(new { message = "A sala informada para vincular a poltrona não existe." });

        return CreatedAtAction(nameof(GetById), new { id = seat.Id }, seat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _seatService.DeleteAsync(id);
        return NoContent();
    }
}