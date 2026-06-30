using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Repositories;

public interface IDestinationRepository : IBaseRepository<Destination>
{
    /// <summary>True if any water batch references this destination (same bounded context).</summary>
    Task<bool> IsReferencedByWaterBatchAsync(int destinationId, CancellationToken cancellationToken);

    /// <summary>True if a destination with the given name already exists.</summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
