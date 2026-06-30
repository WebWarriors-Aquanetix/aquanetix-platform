using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Transform;

public static class CreateDestinationCommandFromResourceAssembler
{
    public static CreateDestinationCommand ToCommandFromResource(CreateDestinationResource resource) =>
        new(resource.Name, resource.Address, resource.Description);
}
