using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDashboardConfiguration(this ModelBuilder builder)
    {
        builder.Entity<QualityAnalysis>().ToTable("quality_analyses");
        builder.Entity<QualityAnalysis>().HasKey(q => q.Id);
        builder.Entity<QualityAnalysis>().Property(q => q.Id)
            .HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Entity<QualityAnalysis>().Property(q => q.SensorSourceId)
            .HasColumnName("sensor_source_id").IsRequired();
        builder.Entity<QualityAnalysis>().Property(q => q.DetectedParameters)
            .HasColumnName("detected_parameters").IsRequired().HasMaxLength(50);
        builder.Entity<QualityAnalysis>().Property(q => q.AnomalyStatus)
            .HasColumnName("anomaly_status").IsRequired().HasMaxLength(20);
        builder.Entity<QualityAnalysis>().Property(q => q.SeverityScore)
            .HasColumnName("severity_score").IsRequired();
        builder.Entity<QualityAnalysis>().Property(q => q.HasContaminationPeakPrediction)
            .HasColumnName("has_contamination_peak_prediction").IsRequired();
        builder.Entity<QualityAnalysis>().Property(q => q.CreatedAt)
            .HasColumnName("created_at");
        builder.Entity<QualityAnalysis>().Property(q => q.UpdatedAt)
            .HasColumnName("updated_at");
        builder.Entity<ThresholdConfiguration>().Property<int?>("DeviceId")
            .HasColumnName("device_id_fk");
        builder.Entity<Device>()
            .HasMany(d => d.Thresholds)
            .WithOne()
            .HasForeignKey("DeviceId")
            .HasConstraintName("FK_threshold_device");
    }
}