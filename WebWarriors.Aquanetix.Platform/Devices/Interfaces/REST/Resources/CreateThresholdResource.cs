namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

public record CreateThresholdResource(
    double MinValue,
    double MaxValue,
    string Unit,
    string AlertLevel);
