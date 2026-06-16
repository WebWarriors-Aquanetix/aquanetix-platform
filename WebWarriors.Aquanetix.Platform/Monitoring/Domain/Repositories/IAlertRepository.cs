using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;

public interface IAlertRepository : IBaseRepository<Alert>
{
    Task<IEnumerable<Alert>> FindByDeviceIdAsync(int deviceId, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> FindActiveByDeviceNameAsync(string deviceName, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> FindByStatusAsync(string status, CancellationToken cancellationToken);
}