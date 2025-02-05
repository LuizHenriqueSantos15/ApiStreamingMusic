using ApiStreamingMusic.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        // Simulando uma validação de usuário (substitua pelo seu banco de dados)
        if (request.Email != "admin" || request.Senha != "1234")
            return Unauthorized("Usuário ou senha inválidos.");

        // Gerar token JWT
        var token = _authService.GenerateToken("1", request.Email);
        return Ok(new { Token = token });
    }
}