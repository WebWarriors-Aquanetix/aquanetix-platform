namespace WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Resources;

public record CreateAlertResource(
    int            DeviceId,
    string         DeviceName,
    string         Location,
    string         Type,
    string         Severity,
    string         Message,
    DateTimeOffset Timestamp,
    string         Status,
    double         Value,
    double         Threshold);