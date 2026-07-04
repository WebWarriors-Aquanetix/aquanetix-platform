namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;

public record ChangePlanCommand(int SubscriptionId, string NewPlan);
