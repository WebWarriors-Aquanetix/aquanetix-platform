namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

public record UpdateDeviceResource(
    string         CurrentStatus,
    DateTimeOffset LastTelemetrySync);
