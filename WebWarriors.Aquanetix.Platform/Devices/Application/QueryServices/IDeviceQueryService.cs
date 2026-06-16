using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Queries;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;

public interface IDeviceQueryService
{
    Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<ThresholdConfiguration>> Handle(GetThresholdsByDeviceIdQuery query, CancellationToken cancellationToken);
}