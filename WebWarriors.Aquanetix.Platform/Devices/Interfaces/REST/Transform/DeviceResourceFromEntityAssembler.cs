using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

public static class DeviceResourceFromEntityAssembler
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        return new DeviceResource(
            entity.Id,
            entity.OwnerId,
            entity.SerialNumber,
            entity.DeviceType.ToString(),
            entity.CurrentStatus.ToString(),
            entity.LastTelemetrySync,
            entity.Name,
            entity.Location,
            entity.Unit,
            entity.CurrentValue,
            entity.DestinationId
        );
    }
}
