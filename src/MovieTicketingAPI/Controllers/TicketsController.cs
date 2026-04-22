using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Services.Tickets;
using System.Security.Claims;

namespace MovieTicketingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveSeat([FromBody] BuyTicketDto dto)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized(new { message = "Usuário não autorizado." });

        var success = await _ticketService.ReserveSeatAsync(userId, dto.MovieSessionId, dto.SeatId);
        if (!success) return Conflict(new { message = "Assento bloqueado para compra neste momento." });

        return Ok(new { message = "Assento reservado para processo de compra." });
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmPurchase([FromBody] BuyTicketDto dto)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized(new { message = "Usuário não autorizado." });

        try
        {
            var ticket = await _ticketService.ConfirmPurchaseAsync(userId, dto);
            if (ticket == null) return NotFound(new { message = "Dados inválidos." });

            return Ok(new { message = "Compra confirmada.", ticket });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
