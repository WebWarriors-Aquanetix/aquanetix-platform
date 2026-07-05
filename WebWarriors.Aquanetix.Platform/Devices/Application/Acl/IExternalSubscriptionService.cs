namespace WebWarriors.Aquanetix.Platform.Devices.Application.Acl;

/// <summary>
///     Devices' anti-corruption layer over the Subscription context.
///     Lets Devices ask about plan limits without depending on Subscription types.
/// </summary>
public interface IExternalSubscriptionService
{
    /// <summary>
    ///     Device limit for the user's plan: >=0 limit, -1 unlimited, null = no subscription.
    /// </summary>
    Task<int?> GetDeviceLimitForUser(int userId);
}
