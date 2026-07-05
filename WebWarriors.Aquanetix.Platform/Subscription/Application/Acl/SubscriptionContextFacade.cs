using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Subscription.Application.Acl;

/// <summary>
///     Implementation of the Subscription context façade. Resolves the user's
///     current plan and returns its device limit using the fixed PlanCatalog.
/// </summary>
public class SubscriptionContextFacade(ISubscriptionRepository subscriptionRepository)
    : ISubscriptionContextFacade
{
    /// <inheritdoc />
    public async Task<int?> GetDeviceLimitForUser(int userId)
    {
        var subscription = await subscriptionRepository.FindByUserIdAsync(userId);
        if (subscription is null)
            return null; // no subscription → caller decides (we block).

        var plan = PlanCatalog.FindByName(subscription.Plan);
        // If the stored plan name is unknown, treat as no allowance (safe default).
        return plan?.MaxDevices ?? 0;
    }
}
