namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;

/// <summary>
///     Immutable definition of a subscription plan. Mirrors the report's
///     SubscriptionPlan class (Name, MonthlyCost, MaxDevices).
///     MaxDevices == -1 means unlimited.
/// </summary>
public record PlanDefinition(string Name, decimal MonthlyCost, int MaxDevices)
{
    public bool IsUnlimited => MaxDevices == -1;

    /// <summary>Validates whether a new device can be registered under this plan.</summary>
    public bool VerifyLimit(int currentDevices) =>
        IsUnlimited || currentDevices < MaxDevices;
}
