using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Infrastructure.Persistence.EFC.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<IEnumerable<Alert>> FindByDeviceIdAsync(int deviceId, CancellationToken cancellationToken)
        => await Context.Set<Alert>()
            .Where(a => a.DeviceId == deviceId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Alert>> FindActiveByDeviceNameAsync(string deviceName, CancellationToken cancellationToken)
        => await Context.Set<Alert>()
            .Where(a => a.DeviceName == deviceName && a.Status == "Activa")
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Alert>> FindByStatusAsync(string status, CancellationToken cancellationToken)
        => await Context.Set<Alert>()
            .Where(a => a.Status == status)
            .ToListAsync(cancellationToken);
}