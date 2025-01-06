using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Repositories.IRepositories;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService = new TokenService();
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = _usuarioRepository.GetUsuarioByEmail(loginRequest.Email);
        if (user != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            var token = _tokenService.GenerateToken(loginRequest.Email, "Admin");
            return Ok(new { email = user.Email, Token = token });
        }

        return Unauthorized("Usuário ou senha inválidos.");
    }

    [HttpGet("refresh-token")]
    public IActionResult RefreshToken([FromQuery] string email)
    {
        var user = _usuarioRepository.GetUsuarioByEmail(email);

        if (user == null)
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        var token = _tokenService.GenerateToken(email, "Admin");
        return Ok(new { email = user.Email, refreshToken = token });
    }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}