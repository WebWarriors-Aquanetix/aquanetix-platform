using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Subscription.Application.Acl;

public class SubscriptionContextFacade(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionCommandService subscriptionCommandService)
    : ISubscriptionContextFacade
{
    public async Task<int?> GetDeviceLimitForUser(int userId)
    {
        var subscription = await subscriptionRepository.FindByUserIdAsync(userId);
        if (subscription is null) return null;
        var plan = PlanCatalog.FindByName(subscription.Plan);
        return plan?.MaxDevices ?? 0;
    }

    public bool IsValidPlan(string plan) => PlanCatalog.IsValidPlan(plan);

    public async Task<int> CreateSubscriptionForUser(int userId, string plan)
    {
        if (!PlanCatalog.IsValidPlan(plan)) return 0;
        var created = await subscriptionCommandService.Handle(
            new CreateSubscriptionCommand(userId, plan, "Active"));
        return created?.Id ?? 0;
    }
}
