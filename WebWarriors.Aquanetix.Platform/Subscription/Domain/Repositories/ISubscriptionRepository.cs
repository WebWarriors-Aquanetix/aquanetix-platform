using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<SubscriptionEntity>
{
    /// <summary>Finds the subscription belonging to a user. Null if none exists.</summary>
    Task<SubscriptionEntity?> FindByUserIdAsync(int userId);
}
