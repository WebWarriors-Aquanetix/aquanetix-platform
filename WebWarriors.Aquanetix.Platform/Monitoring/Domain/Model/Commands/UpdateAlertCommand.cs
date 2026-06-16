namespace WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Commands;

public record UpdateAlertCommand(
    int           Id,
    int           DeviceId,
    string        DeviceName,
    string        Location,
    string        Type,
    string        Severity,
    string        Message,
    DateTimeOffset Timestamp,
    string        Status,
    double        Value,
    double        Threshold);