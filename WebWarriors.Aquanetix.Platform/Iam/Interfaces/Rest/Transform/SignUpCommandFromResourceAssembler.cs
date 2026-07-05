using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource) =>
        new(resource.Email, resource.Password, resource.Plan);
}
