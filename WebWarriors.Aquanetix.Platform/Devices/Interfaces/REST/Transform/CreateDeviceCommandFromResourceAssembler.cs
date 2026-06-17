using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

public static class CreateDeviceCommandFromResourceAssembler
{
    public static CreateDeviceCommand ToCommandFromResource(CreateDeviceResource resource) =>
        new(
            resource.OwnerId,
            resource.SerialNumber,
            Enum.Parse<DeviceType>(resource.DeviceType, ignoreCase: true),
            DeviceStatus.Normal,
            DateTimeOffset.UtcNow,
            resource.Name,
            resource.Location,
            resource.Unit,
            resource.CurrentValue);
}
