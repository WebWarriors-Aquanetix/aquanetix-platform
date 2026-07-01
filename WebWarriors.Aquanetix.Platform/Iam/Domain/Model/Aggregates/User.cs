using WebWarriors.Aquanetix.Platform.Shared.Domain.Model.Entities;

namespace WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;

public class User : IAuditableEntity
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    protected User()
    {
        Email = string.Empty;
        PasswordHash = string.Empty;
        Role = "User";
    }

    public User(string email, string passwordHash, string role = "User")
    {
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        Role = string.IsNullOrWhiteSpace(role) ? "User" : role.Trim();
    }
}
