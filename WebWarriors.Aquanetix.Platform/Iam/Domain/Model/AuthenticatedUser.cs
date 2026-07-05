namespace WebWarriors.Aquanetix.Platform.Iam.Domain.Model;

/// <summary>Authenticated user with the issued JWT token.</summary>
public record AuthenticatedUser(int Id, string Email, string Role, string Token);
