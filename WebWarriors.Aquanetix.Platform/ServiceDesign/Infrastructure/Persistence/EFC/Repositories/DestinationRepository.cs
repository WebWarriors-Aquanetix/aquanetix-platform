using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Infrastructure.Persistence.EFC.Repositories;

public class DestinationRepository(AppDbContext context)
    : BaseRepository<Destination>(context), IDestinationRepository
{
    /// <summary>True if any water batch (same bounded context) references this destination.</summary>
    public async Task<bool> IsReferencedByWaterBatchAsync(int destinationId, CancellationToken cancellationToken)
        => await Context.Set<WaterBatch>()
            .AnyAsync(w => w.DestinationSectorId == destinationId, cancellationToken);

    /// <summary>True if a destination with the given name already exists.</summary>
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await Context.Set<Destination>()
            .AnyAsync(d => d.Name == name, cancellationToken);
}
