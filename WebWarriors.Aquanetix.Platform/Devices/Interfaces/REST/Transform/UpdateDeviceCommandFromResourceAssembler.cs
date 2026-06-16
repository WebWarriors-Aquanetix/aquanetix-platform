using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

public static class UpdateDeviceCommandFromResourceAssembler
{
    public static UpdateDeviceCommand ToCommandFromResource(UpdateDeviceResource resource, int deviceId) =>
        new(deviceId,
            Enum.Parse<DeviceStatus>(resource.CurrentStatus),
            resource.LastTelemetrySync);
}
