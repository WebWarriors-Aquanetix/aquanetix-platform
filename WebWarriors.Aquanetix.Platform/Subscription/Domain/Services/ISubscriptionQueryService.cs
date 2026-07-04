using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Queries;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<SubscriptionEntity?> Handle(GetSubscriptionByIdQuery query);
    Task<IEnumerable<SubscriptionEntity>> Handle(GetAllSubscriptionsQuery query);
}
