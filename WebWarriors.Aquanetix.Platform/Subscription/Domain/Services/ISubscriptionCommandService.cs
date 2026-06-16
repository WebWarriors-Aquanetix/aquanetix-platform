using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;

namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription?>
        Handle(CreateSubscriptionCommand command);
    Task<WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription?>
        Handle(CancelSubscriptionCommand command);
}