using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Transform;

public static class DestinationResourceFromEntityAssembler
{
    public static DestinationResource ToResourceFromEntity(Destination entity) =>
        new(entity.Id, entity.Name, entity.Address, entity.Description);
}
