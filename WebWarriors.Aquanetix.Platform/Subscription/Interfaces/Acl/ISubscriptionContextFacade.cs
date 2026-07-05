namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

/// <summary>
///     Public façade exposing subscription/plan info to other bounded contexts.
///     Other contexts depend on this contract, never on Subscription internals.
/// </summary>
public interface ISubscriptionContextFacade
{
    /// <summary>
    ///     Returns the device limit allowed by the user's current plan:
    ///       >= 0  → the maximum number of devices allowed,
    ///       -1    → unlimited,
    ///       null  → the user has no subscription.
    /// </summary>
    Task<int?> GetDeviceLimitForUser(int userId);
}
