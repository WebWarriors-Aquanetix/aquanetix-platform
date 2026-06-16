using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Dashboard.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.Devices.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.Monitoring.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Subscription.Infrastructure.Persistence.EFC.Configuration.Extensions;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates;
namespace WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Alert> Alerts { get; set; }

    public DbSet<
            WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription>
        Subscriptions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();

        builder.ApplyServiceDesignConfiguration();
        builder.ApplyMonitoringConfiguration();
        builder.ApplyDashboardConfiguration();
        builder.ApplyDevicesConfiguration();

        builder.ApplySubscriptionConfiguration();
    }
}