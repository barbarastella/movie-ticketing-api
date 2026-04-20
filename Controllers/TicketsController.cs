using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Services;
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

    [HttpPost]
    public async Task<IActionResult> BuyAsync([FromBody] BuyTicketDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            return Unauthorized(new { message = "Token inválido ou usário não identificado." });

        var ticket = await _ticketService.BuyAsync(userId, dto);

        if (ticket == null) return NotFound(new { message = "Filme não encontrado." });

        return Ok(new { message = "Ingresso comprado com sucesso!", ticket = ticket });
    }
}
