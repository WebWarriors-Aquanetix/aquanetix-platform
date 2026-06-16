namespace WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;

public record CreateSubscriptionCommand(
    int UserId,
    string Plan,
    string Status
);