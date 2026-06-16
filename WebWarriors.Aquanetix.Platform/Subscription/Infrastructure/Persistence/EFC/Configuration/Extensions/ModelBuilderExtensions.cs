using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates;

namespace WebWarriors.Aquanetix.Platform.Subscription.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions").ToTable("subscriptions");

        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions").HasKey(s => s.Id);

        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions")
            .Property(s => s.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions")
            .Property(s => s.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions")
            .Property(s => s.Plan)
            .HasColumnName("plan")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<
                WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>()
            .ToTable("subscriptions")
            .Property(s => s.Status)
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();
    }
}