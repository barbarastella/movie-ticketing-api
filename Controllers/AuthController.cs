using Microsoft.AspNetCore.Mvc;
using MovieTicketingAPI.Services;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = await _authService.RegisterAsync(dto);
        if (user == null) return Conflict(new { message = "Email já está em uso" });

        return Ok(new { message = "Cadastro realizado com sucesso" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null) return Unauthorized(new { message = "Credenciais inválidas" });

        return Ok(new { token });
    }
}
