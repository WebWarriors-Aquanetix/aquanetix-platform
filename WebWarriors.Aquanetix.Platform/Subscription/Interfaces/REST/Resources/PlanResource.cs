namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;

/// <summary>Represents a subscription plan from the catalog. maxDevices = -1 means unlimited.</summary>
public record PlanResource(
    string Name,
    decimal MonthlyCost,
    int MaxDevices,
    bool IsUnlimited
);
