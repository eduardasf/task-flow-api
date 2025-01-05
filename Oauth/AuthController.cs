using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Domains;
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
            return Ok(new {Email = user.Email, Token = token });
        }

        return Unauthorized("Usuário ou senha inválidos.");
    }

    [HttpPost("refresh-token")]
    [Authorize]
    public IActionResult RefreshToken([FromQuery] string token)
    {
        var validator = new TokenValidator();

        // Valida o token atual
        var claimsPrincipal = validator.ValidateToken(token);
        if (claimsPrincipal == null)
        {
            return Unauthorized("Token inválido ou expirado.");
        }

        // Gere um novo token usando as informações do token anterior
        var email = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

        if (email == null || role == null)
        {
            return Unauthorized("Não foi possível gerar um novo token.");
        }

        var newToken = _tokenService.GenerateToken(email, role);

        return Ok(new { Email = email, Token = newToken });
    }

}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
