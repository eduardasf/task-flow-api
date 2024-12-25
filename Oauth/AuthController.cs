using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService = new TokenService();

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // Substituir pela validação real do usuário
        if (loginRequest.Username == "admin" && loginRequest.Password == "password")
        {
            var token = _tokenService.GenerateToken(loginRequest.Username, "Admin");
            return Ok(new { Token = token });
        }

        return Unauthorized("Usuário ou senha inválidos.");
    }

    [HttpGet("validate")]
    public IActionResult ValidateToken([FromQuery] string token)
    {
        var validator = new TokenValidator();
        var claimsPrincipal = validator.ValidateToken(token);

        if (claimsPrincipal != null)
        {
            return Ok("Token válido.");
        }

        return Unauthorized("Token inválido.");
    }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
