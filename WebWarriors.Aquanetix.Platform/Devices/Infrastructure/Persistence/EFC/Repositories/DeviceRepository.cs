using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace WebWarriors.Aquanetix.Platform.Devices.Infrastructure.Persistence.EFC.Repositories;

public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
{
    public DeviceRepository(AppDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task RemoveThresholdsByDeviceId(int deviceId, CancellationToken cancellationToken)
    {
        // ThresholdConfiguration.SensorId holds the owning device's id.
        var thresholds = await Context.Set<ThresholdConfiguration>()
            .Where(t => t.SensorId == deviceId)
            .ToListAsync(cancellationToken);

        if (thresholds.Count > 0)
            Context.Set<ThresholdConfiguration>().RemoveRange(thresholds);
    }

    /// <inheritdoc />
    public async Task<int> CountByOwnerIdAsync(int ownerId, CancellationToken cancellationToken)
    {
        return await Context.Set<Device>()
            .CountAsync(d => d.OwnerId == ownerId, cancellationToken);
    }
}
