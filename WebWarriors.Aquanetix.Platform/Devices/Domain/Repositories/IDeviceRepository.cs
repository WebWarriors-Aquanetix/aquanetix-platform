using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;

public interface IDeviceRepository : IBaseRepository<Device>
{
    Task RemoveThresholdsByDeviceId(int deviceId, CancellationToken cancellationToken);

    /// <summary>Counts how many devices belong to a given owner.</summary>
    Task<int> CountByOwnerIdAsync(int ownerId, CancellationToken cancellationToken);
}
