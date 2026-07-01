using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(AuthenticatedUser authenticatedUser)
    {
        return new AuthenticatedUserResource(
            authenticatedUser.Id,
            authenticatedUser.Email,
            authenticatedUser.Role);
    }
}
