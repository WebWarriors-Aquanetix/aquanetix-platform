namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

/// <summary>
///     Token settings, bound from appsettings.json ("TokenSettings").
///     Secret is optional here; TokenService falls back to a default if unset
///     (demo-safe). In production, always configure it.
/// </summary>
public class TokenSettings
{
    public string Secret { get; set; } = string.Empty;
}
