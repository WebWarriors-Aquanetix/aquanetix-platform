namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;

public record CreateDeviceResource(
    int    OwnerId,
    string SerialNumber,
    string DeviceType,
    string Name,
    string Location,
    string Unit,
    double CurrentValue
);
