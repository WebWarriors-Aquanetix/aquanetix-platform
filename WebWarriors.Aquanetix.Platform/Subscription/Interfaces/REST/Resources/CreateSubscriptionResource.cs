namespace WebWarriors.Aquanetix.Platform.Subscription.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
    int UserId,
    string Plan,
    string Status
);