using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Transform;

public static class PlanResourceFromDefinitionAssembler
{
    public static PlanResource ToResource(PlanDefinition definition) =>
        new(definition.Name, definition.MonthlyCost, definition.MaxDevices, definition.IsUnlimited);
}
