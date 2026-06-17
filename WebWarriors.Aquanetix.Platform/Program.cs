using WebWarriors.Aquanetix.Platform.Subscription.Application.Internal.CommandServices;
using WebWarriors.Aquanetix.Platform.Subscription.Application.Internal.QueryServices;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using WebWarriors.Aquanetix.Platform.Subscription.Infrastructure.Persistence.EFC.Repositories;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using MySql.Data.MySqlClient;
using WebWarriors.Aquanetix.Platform.Dashboard.Application.Internal.QueryServices;
using WebWarriors.Aquanetix.Platform.Dashboard.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Dashboard.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.Devices.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Devices.Application.Internal.CommandServices;
using WebWarriors.Aquanetix.Platform.Devices.Application.Internal.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Devices.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Internal.CommandServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Internal.QueryServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.Internal.CommandServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.Internal.QueryServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using WebWarriors.Aquanetix.Platform.Shared.Resources;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;
using ProblemDetailsFactory =
    WebWarriors.Aquanetix.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services
    .AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Connection string is not configured.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "WebWarriors.Aquanetix.Platform",
        Version     = "v1",
        Description = "Aquanetix IoT Water Quality Monitoring Platform API"
    });
    options.EnableAnnotations();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Dashboard
builder.Services.AddScoped<IQualityAnalysisRepository, QualityAnalysisRepository>();
builder.Services.AddScoped<IQualityAnalysisQueryService, QualityAnalysisQueryService>();
builder.Services.AddScoped<IWaterBatchRepository, WaterBatchRepository>();
builder.Services.AddScoped<IWaterBatchQueryService, WaterBatchQueryService>();
builder.Services.AddScoped<IWaterBatchCommandService, WaterBatchCommandService>();

// Monitoring (Alerts)
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();

// Devices
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();

builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator([typeof(Program)]);

// Subscription
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        //context.Database.Migrate();
    }
    catch (MySql.Data.MySqlClient.MySqlException ex) when (ex.Message.Contains("database exists"))
    {
        // Base ya existe, continuar normalmente
    }
}

app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();