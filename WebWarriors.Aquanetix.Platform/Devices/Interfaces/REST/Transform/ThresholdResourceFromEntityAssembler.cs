using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

public static class ThresholdResourceFromEntityAssembler
{
    public static ThresholdResource ToResourceFromEntity(ThresholdConfiguration entity) =>
        new(entity.Id,
            entity.SensorId,
            entity.MinValue,
            entity.MaxValue,
            entity.Unit,
            entity.AlertLevel.ToString());
}
