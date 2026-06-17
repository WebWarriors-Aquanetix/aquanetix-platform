using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;

namespace WebWarriors.Aquanetix.Platform.Devices.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesConfiguration(this ModelBuilder builder)
    {
        // ── Device aggregate ──────────────────────────────────────────────
        builder.Entity<Device>().ToTable("devices");
        builder.Entity<Device>().HasKey(d => d.Id);
        builder.Entity<Device>().Property(d => d.Id)
            .HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(d => d.OwnerId)
            .HasColumnName("owner_id").IsRequired();
        builder.Entity<Device>().Property(d => d.SerialNumber)
            .HasColumnName("serial_number").IsRequired().HasMaxLength(100);
        builder.Entity<Device>().Property(d => d.DeviceType)
            .HasColumnName("device_type").IsRequired();
        builder.Entity<Device>().Property(d => d.CurrentStatus)
            .HasColumnName("current_status").IsRequired();
        builder.Entity<Device>().Property(d => d.LastTelemetrySync)
            .HasColumnName("last_telemetry_sync").IsRequired();
        builder.Entity<Device>().Property(d => d.CreatedAt)
            .HasColumnName("created_at");
        builder.Entity<Device>().Property(d => d.UpdatedAt)
            .HasColumnName("updated_at");

        // ── ThresholdConfiguration entity ─────────────────────────────────
        builder.Entity<ThresholdConfiguration>().ToTable("threshold_configurations");
        builder.Entity<ThresholdConfiguration>().HasKey(t => t.Id);
        builder.Entity<ThresholdConfiguration>().Property(t => t.Id)
            .HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ThresholdConfiguration>().Property(t => t.SensorId)
            .HasColumnName("sensor_id").IsRequired();
        builder.Entity<ThresholdConfiguration>().Property(t => t.MinValue)
            .HasColumnName("min_value").IsRequired();
        builder.Entity<ThresholdConfiguration>().Property(t => t.MaxValue)
            .HasColumnName("max_value").IsRequired();
        builder.Entity<ThresholdConfiguration>().Property(t => t.Unit)
            .HasColumnName("unit").IsRequired().HasMaxLength(20);
        builder.Entity<ThresholdConfiguration>().Property(t => t.AlertLevel)
            .HasColumnName("alert_level").IsRequired();
        builder.Entity<Device>()
            .HasMany(d => d.Thresholds)
            .WithOne()
            .HasForeignKey("DeviceId")
            .HasConstraintName("FK_threshold_device");

        builder.Entity<ThresholdConfiguration>()
            .Property<int?>("DeviceId")
            .HasColumnName("device_id_fk");
    }
}
