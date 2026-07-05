using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Application.Internal.QueryServices;

public class SubscriptionQueryService : ISubscriptionQueryService
{
    private readonly ISubscriptionRepository repository;

    public SubscriptionQueryService(ISubscriptionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<SubscriptionEntity?> Handle(GetSubscriptionByIdQuery query)
    {
        return await repository.FindByIdAsync(query.SubscriptionId);
    }

    public async Task<IEnumerable<SubscriptionEntity>> Handle(GetAllSubscriptionsQuery query)
    {
        return await repository.ListAsync();
    }

    public async Task<SubscriptionEntity?> Handle(GetSubscriptionByUserIdQuery query)
    {
        return await repository.FindByUserIdAsync(query.UserId);
    }
}
