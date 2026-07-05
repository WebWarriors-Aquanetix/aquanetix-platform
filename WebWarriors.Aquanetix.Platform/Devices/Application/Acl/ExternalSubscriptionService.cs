using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Acl;

/// <summary>
///     Implementation of Devices' ACL over Subscription. Depends only on the
///     public Subscription façade contract, never on Subscription internals.
/// </summary>
public class ExternalSubscriptionService(ISubscriptionContextFacade subscriptionContextFacade)
    : IExternalSubscriptionService
{
    /// <inheritdoc />
    public async Task<int?> GetDeviceLimitForUser(int userId)
        => await subscriptionContextFacade.GetDeviceLimitForUser(userId);
}
