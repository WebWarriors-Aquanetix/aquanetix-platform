namespace WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;

/// <summary>Sign up command: email, password and chosen subscription plan.</summary>
public record SignUpCommand(string Email, string Password, string Plan);
