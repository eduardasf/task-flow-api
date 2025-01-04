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

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true, // Aqui verifica se o token está expirado
                ClockSkew = TimeSpan.Zero
            }, out _);

            return claimsPrincipal;
        }
        catch (SecurityTokenExpiredException)
        {
            // Se o token estiver expirado, retorna null ou ajusta para continuar com a validação
            return null;
        }
        catch
        {
            return null;
        }
    }
}

