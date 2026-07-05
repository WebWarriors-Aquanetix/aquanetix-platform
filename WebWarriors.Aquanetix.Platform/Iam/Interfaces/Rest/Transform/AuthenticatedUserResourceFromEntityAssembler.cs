using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(AuthenticatedUser user) =>
        new(user.Id, user.Email, user.Role, user.Token);
}
