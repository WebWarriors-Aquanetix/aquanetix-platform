using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;

public record CreateThresholdCommand(
    int        DeviceId,
    double     MinValue,
    double     MaxValue,
    string     Unit,
    AlertLevel AlertLevel);
