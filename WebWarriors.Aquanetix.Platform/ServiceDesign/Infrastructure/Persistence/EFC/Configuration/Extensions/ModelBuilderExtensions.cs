using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyServiceDesignConfiguration(this ModelBuilder builder)
    {
        builder.Entity<WaterBatch>().ToTable("water_batches");
        builder.Entity<WaterBatch>().HasKey(w => w.Id);
        builder.Entity<WaterBatch>().Property(w => w.Id)
            .HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WaterBatch>().Property(w => w.CertificationCode)
            .HasColumnName("certification_code").IsRequired().HasMaxLength(50);
        builder.Entity<WaterBatch>().Property(w => w.DestinationSectorId)
            .HasColumnName("destination_sector_id").IsRequired();
        builder.Entity<WaterBatch>().Property(w => w.VolumeLiters)
            .HasColumnName("volume_liters").IsRequired();
        builder.Entity<WaterBatch>().Property(w => w.DeliveryTimestamp)
            .HasColumnName("delivery_timestamp").IsRequired();
        builder.Entity<WaterBatch>().Property(w => w.Status)
            .HasColumnName("status").IsRequired().HasMaxLength(20);
        builder.Entity<WaterBatch>().Property(w => w.Source)
            .HasColumnName("source").IsRequired().HasMaxLength(150);
        builder.Entity<WaterBatch>().Property(w => w.CreatedAt)
            .HasColumnName("created_at");
        builder.Entity<WaterBatch>().Property(w => w.UpdatedAt)
            .HasColumnName("updated_at");
        builder.Entity<Destination>().ToTable("destinations");
        builder.Entity<Destination>().HasKey(d => d.Id);
        builder.Entity<Destination>().Property(d => d.Id)
            .HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Destination>().Property(d => d.Name)
            .HasColumnName("name").IsRequired().HasMaxLength(120);
        builder.Entity<Destination>().HasIndex(d => d.Name).IsUnique();
        builder.Entity<Destination>().Property(d => d.Address)
            .HasColumnName("address").HasMaxLength(250);
        builder.Entity<Destination>().Property(d => d.Description)
            .HasColumnName("description").HasMaxLength(500);
        builder.Entity<Destination>().Property(d => d.CreatedAt)
            .HasColumnName("created_at");
        builder.Entity<Destination>().Property(d => d.UpdatedAt)
            .HasColumnName("updated_at");

    }
}