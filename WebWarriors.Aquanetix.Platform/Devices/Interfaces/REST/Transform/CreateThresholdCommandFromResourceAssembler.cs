using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

public static class CreateThresholdCommandFromResourceAssembler
{
    public static CreateThresholdCommand ToCommandFromResource(CreateThresholdResource resource, int deviceId) =>
        new(deviceId,
            resource.MinValue,
            resource.MaxValue,
            resource.Unit,
            Enum.Parse<AlertLevel>(resource.AlertLevel));
}
