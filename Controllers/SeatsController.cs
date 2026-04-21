using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models.DTOs;
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
        if (seat == null) return NotFound(new { message = "Assento não encontrado." });

        return Ok(seat);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSeatDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.SeatNumber)) return BadRequest(new { message = "O número do assento é obrigatório (ex: A1)." });

        var seat = await _seatService.CreateAsync(dto.SeatNumber, dto.RoomId);
        if (seat == null) return NotFound(new { message = "A sala informada para vincular o assento não existe." });

        return CreatedAtAction(nameof(GetById), new { id = seat.Id }, seat);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSeatDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.SeatNumber)) return BadRequest(new { message = "O número do assento é obrigatório." });

        var updatedSeat = await _seatService.UpdateAsync(id, dto);
        if (updatedSeat == null) return NotFound(new { message = "Assento não encontrado ou a Sala informada é inválida." });

        return Ok(updatedSeat);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _seatService.DeleteAsync(id);
        return NoContent();
    }
}