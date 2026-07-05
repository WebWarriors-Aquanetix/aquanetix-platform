using System.Security.Claims;
using System.Text;
using WebWarriors.Aquanetix.Platform.Iam.Application.Internal.OutboundServices;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Tokens.Jwt.Services;

/// <summary>Generates and validates JWT tokens.</summary>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    // Fallback secret so JWT works even if appsettings isn't configured (demo-safe).
    // For production, always set TokenSettings:Secret in appsettings.
    private string Secret => string.IsNullOrWhiteSpace(_tokenSettings?.Secret)
        ? "aquanetix-super-secret-key-change-me-please-1234567890"
        : _tokenSettings.Secret;

    public string GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }

    public async Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;
        var key = Encoding.ASCII.GetBytes(Secret);
        try
        {
            var tokenHandler = new JsonWebTokenHandler();
            var result = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });
            if (!result.IsValid) return null;
            var jwt = (JsonWebToken)result.SecurityToken;
            var sid = jwt.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            return int.Parse(sid);
        }
        catch
        {
            return null;
        }
    }
}
