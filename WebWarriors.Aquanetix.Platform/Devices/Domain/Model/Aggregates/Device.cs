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

    // ── Campos de presentación / telemetría (mínimo viable) ───────────
    public string Name { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;
    public string Unit { get; private set; } = string.Empty;
    public double CurrentValue { get; private set; }

    /// <summary>FK to a ServiceDesign Destination (the site where the device is installed).
    /// Nullable until Feature 3 makes it mandatory and wires the UI.</summary>
    public int? DestinationId { get; private set; }

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

    public Device(int ownerId, string serialNumber, DeviceType deviceType,
        string name, string location, string unit, double currentValue)
        : this(ownerId, serialNumber, deviceType)
    {
        Name = name ?? string.Empty;
        Location = location ?? string.Empty;
        Unit = unit ?? string.Empty;
        CurrentValue = currentValue;
    }

    // ctor sin parámetros para materialización de EF Core
    protected Device() { }

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

    /// <summary>Registra una nueva lectura de telemetría.</summary>
    public void RecordReading(double value)
    {
        CurrentValue = value;
        LastTelemetrySync = DateTimeOffset.UtcNow;
    }

    /// <summary>Updates the device status, telemetry sync and presentation fields.</summary>
    public void Update(UpdateDeviceCommand command)
    {
        CurrentStatus      = command.CurrentStatus;
        LastTelemetrySync  = command.LastTelemetrySync;

        if (command.Name is not null)         Name = command.Name;
        if (command.Location is not null)     Location = command.Location;
        if (command.Unit is not null)         Unit = command.Unit;
        if (command.CurrentValue is not null) CurrentValue = command.CurrentValue.Value;
        if (command.DestinationId is not null) DestinationId = command.DestinationId;
    }
}
