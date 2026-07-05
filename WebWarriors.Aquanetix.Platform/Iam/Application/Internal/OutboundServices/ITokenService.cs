using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;

namespace WebWarriors.Aquanetix.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    /// <summary>Generates a JWT for the given user.</summary>
    string GenerateToken(User user);

    /// <summary>Validates a JWT and returns the user id, or null if invalid.</summary>
    Task<int?> ValidateToken(string token);
}
