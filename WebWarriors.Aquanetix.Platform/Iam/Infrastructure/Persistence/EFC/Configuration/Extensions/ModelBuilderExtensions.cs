using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;

namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(user => user.Id);
        builder.Entity<User>().Property(user => user.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<User>().Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(256)
            .IsRequired();
        builder.Entity<User>().HasIndex(user => user.Email)
            .IsUnique();
        builder.Entity<User>().Property(user => user.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(512)
            .IsRequired();
        builder.Entity<User>().Property(user => user.Role)
            .HasColumnName("role")
            .HasMaxLength(50)
            .HasDefaultValue("User")
            .IsRequired();
        builder.Entity<User>().Property(user => user.CreatedAt)
            .HasColumnName("created_at");
        builder.Entity<User>().Property(user => user.UpdatedAt)
            .HasColumnName("updated_at");
    }
}
