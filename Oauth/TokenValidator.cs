using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenValidator
{
    private const string SecretKey = "b3c3655d9deb26d2fdefd177ca618015";

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // Use se precisar validar o emissor
            ValidateAudience = false, // Use se precisar validar a audiência
            ClockSkew = TimeSpan.Zero // Remove tolerância de tempo
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null; // Retorne nulo se o token for inválido
        }
    }
}
