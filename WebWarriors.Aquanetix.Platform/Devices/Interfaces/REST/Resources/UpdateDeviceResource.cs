namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

public record UpdateDeviceResource(
    string         CurrentStatus,
    DateTimeOffset LastTelemetrySync,
    string?        Name         = null,
    string?        Location     = null,
    string?        Unit         = null,
    double?        CurrentValue = null);
