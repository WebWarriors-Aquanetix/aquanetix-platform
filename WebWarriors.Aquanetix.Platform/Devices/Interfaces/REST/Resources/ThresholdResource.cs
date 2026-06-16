namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

public record ThresholdResource(
    int    Id,
    int    DeviceId,
    double MinValue,
    double MaxValue,
    string Unit,
    string AlertLevel);
