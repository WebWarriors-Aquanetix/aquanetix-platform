using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<SubscriptionEntity>(context), ISubscriptionRepository
{
    /// <inheritdoc />
    public async Task<SubscriptionEntity?> FindByUserIdAsync(int userId)
    {
        return await Context.Set<SubscriptionEntity>()
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }
}
