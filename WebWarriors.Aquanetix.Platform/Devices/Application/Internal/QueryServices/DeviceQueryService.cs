using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Internal.QueryServices;

public class DeviceQueryService(IDeviceRepository deviceRepository, AppDbContext context) : IDeviceQueryService
{
    public async Task<Device?> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
        => await deviceRepository.FindByIdAsync(query.DeviceId, cancellationToken);

    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query, CancellationToken cancellationToken)
        => await deviceRepository.ListAsync(cancellationToken);

    public async Task<IEnumerable<ThresholdConfiguration>> Handle(GetThresholdsByDeviceIdQuery query, CancellationToken cancellationToken)
        => await context.Set<ThresholdConfiguration>()
            .Where(t => t.SensorId == query.DeviceId)
            .ToListAsync(cancellationToken);
}