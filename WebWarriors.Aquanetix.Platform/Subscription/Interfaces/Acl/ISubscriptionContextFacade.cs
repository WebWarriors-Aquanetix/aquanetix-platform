namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

public interface ISubscriptionContextFacade
{
    Task<int?> GetDeviceLimitForUser(int userId);

    /// <summary>True if the plan name exists in the catalog.</summary>
    bool IsValidPlan(string plan);

    /// <summary>Creates an active subscription for a user. Returns new id, or 0 if plan invalid.</summary>
    Task<int> CreateSubscriptionForUser(int userId, string plan);
}
