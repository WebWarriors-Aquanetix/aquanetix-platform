using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;

public interface IDeviceRepository : IBaseRepository<Device>
{
    Task RemoveThresholdsByDeviceId(int deviceId, CancellationToken cancellationToken);
}