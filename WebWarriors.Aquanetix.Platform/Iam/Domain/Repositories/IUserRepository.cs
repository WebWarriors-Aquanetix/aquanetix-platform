using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Iam.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
}
