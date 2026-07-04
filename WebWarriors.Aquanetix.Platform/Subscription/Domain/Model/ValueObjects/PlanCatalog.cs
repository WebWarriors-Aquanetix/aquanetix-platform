namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;

/// <summary>
///     Fixed catalog of the subscription plans offered by Aquanetix.
///     Kept in code (not in the DB) because the set of plans is small and stable.
///     Prices are in PEN (S/). MaxDevices == -1 means unlimited.
/// </summary>
public static class PlanCatalog
{
    public const string Basic      = "Basic";
    public const string SmartCity  = "Smart City";
    public const string Industrial = "Industrial";

    public static readonly IReadOnlyList<PlanDefinition> All = new List<PlanDefinition>
    {
        new(Basic,      99m,  10),
        new(SmartCity,  299m, 35),
        new(Industrial, 799m, -1),
    };

    /// <summary>Finds a plan definition by name (case-insensitive). Null if unknown.</summary>
    public static PlanDefinition? FindByName(string name) =>
        All.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <summary>True if the given plan name exists in the catalog.</summary>
    public static bool IsValidPlan(string name) => FindByName(name) is not null;
}
