using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;

public record CreateDeviceCommand(
    int OwnerId,
    string SerialNumber,
    DeviceType DeviceType,
    DeviceStatus CurrentStatus,
    DateTimeOffset? LastTelemetrySync,
    string? Name,
    string? Location,
    string? Unit,
    double? CurrentValue,
    int? DestinationId = null
);
