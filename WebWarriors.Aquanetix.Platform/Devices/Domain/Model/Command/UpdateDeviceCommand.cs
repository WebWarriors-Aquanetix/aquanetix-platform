using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;

public record UpdateDeviceCommand(
    int          Id,
    DeviceStatus CurrentStatus,
    DateTimeOffset LastTelemetrySync);
