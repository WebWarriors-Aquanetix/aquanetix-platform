using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommand(
        CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(
            resource.UserId,
            resource.Plan,
            resource.Status
        );
    }
}