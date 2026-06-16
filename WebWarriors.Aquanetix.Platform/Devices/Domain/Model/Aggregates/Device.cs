using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Model.Entities;

namespace WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;

public class Device : IAuditableEntity
{
    public int Id { get; private set; }
    public int OwnerId { get; private set; }
    public string SerialNumber { get; private set; }
    public DeviceType DeviceType { get; private set; }
    public DeviceStatus CurrentStatus { get; private set; }
    public DateTimeOffset LastTelemetrySync { get; private set; }

    public ICollection<ThresholdConfiguration> Thresholds { get; private set; }
        = new List<ThresholdConfiguration>();

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Device(int ownerId, string serialNumber, DeviceType deviceType)
    {
        OwnerId = ownerId;
        SerialNumber = serialNumber;
        DeviceType = deviceType;
        CurrentStatus = DeviceStatus.Normal;
        LastTelemetrySync = DateTimeOffset.UtcNow;
    }

    public void UpdateStatus(DeviceStatus newStatus)
    {
        CurrentStatus = newStatus;
        LastTelemetrySync = DateTimeOffset.UtcNow;
    }

    public void GoOffline()
    {
        CurrentStatus = DeviceStatus.Offline;
    }

    public void AddThreshold(ThresholdConfiguration threshold)
    {
        Thresholds.Add(threshold);
    }

    /// <summary>Updates the device monitoring frequency and status.</summary>
    public void Update(UpdateDeviceCommand command)
    {
        CurrentStatus      = command.CurrentStatus;
        LastTelemetrySync  = command.LastTelemetrySync;
    }
}