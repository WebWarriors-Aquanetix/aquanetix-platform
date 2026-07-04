using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<SubscriptionEntity?> Handle(CreateSubscriptionCommand command);
    Task<SubscriptionEntity?> Handle(CancelSubscriptionCommand command);
    Task<SubscriptionEntity?> Handle(RenewSubscriptionCommand command);
    Task<SubscriptionEntity?> Handle(ChangePlanCommand command);
}
