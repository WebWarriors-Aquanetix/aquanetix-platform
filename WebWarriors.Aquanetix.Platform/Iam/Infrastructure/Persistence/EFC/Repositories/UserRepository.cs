using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return await Context.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail, cancellationToken);
    }
}
