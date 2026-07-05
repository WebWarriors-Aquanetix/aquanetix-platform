namespace WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Role, string Token);
