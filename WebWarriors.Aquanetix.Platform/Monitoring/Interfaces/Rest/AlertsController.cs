using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Resources;
using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Transform;
using WebWarriors.Aquanetix.Platform.Shared.Interfaces.Rest.ProblemDetails;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Alert endpoints")]
[AllowAnonymous]     
public class AlertsController(
    IAlertQueryService alertQueryService,
    IAlertCommandService alertCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet]
    [SwaggerOperation(Summary = "Get all alerts", OperationId = "GetAllAlerts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alerts retrieved", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAllAlerts(CancellationToken cancellationToken)
    {
        var alerts = await alertQueryService.Handle(new GetAllAlertsQuery(), cancellationToken);
        return Ok(alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{alertId:int}")]
    [SwaggerOperation(Summary = "Get alert by id", OperationId = "GetAlertById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alert found", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Alert not found")]
    public async Task<IActionResult> GetAlertById([FromRoute] int alertId, CancellationToken cancellationToken)
    {
        var alert = await alertQueryService.Handle(new GetAlertByIdQuery(alertId), cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromGetAlertByIdResult(
            this, alert, _errorLocalizer, _problemDetailsFactory,
            found => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpGet("device/{deviceId:int}")]
    [SwaggerOperation(Summary = "Get alerts by device id", OperationId = "GetAlertsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alerts found", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAlertsByDeviceId([FromRoute] int deviceId, CancellationToken cancellationToken)
    {
        var alerts = await alertQueryService.Handle(new GetAlertsByDeviceIdQuery(deviceId), cancellationToken);
        return Ok(alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("status/{status}")]
    [SwaggerOperation(Summary = "Get alerts by status", OperationId = "GetAlertsByStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alerts found", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAlertsByStatus([FromRoute] string status, CancellationToken cancellationToken)
    {
        var alerts = await alertQueryService.Handle(new GetAlertsByStatusQuery(status), cancellationToken);
        return Ok(alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create an alert", OperationId = "CreateAlert")]
    [SwaggerResponse(StatusCodes.Status201Created, "Alert created", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    public async Task<IActionResult> CreateAlert([FromBody] CreateAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result  = await alertCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromCreateAlertResult(
            this, result, _errorLocalizer, _problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAlertById),
                new { alertId = created.Id },
                AlertResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPut("{alertId:int}")]
    [SwaggerOperation(Summary = "Update an alert (resolve)", OperationId = "UpdateAlert")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alert updated", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Alert not found")]
    public async Task<IActionResult> UpdateAlert([FromRoute] int alertId,
        [FromBody] UpdateAlertResource resource, CancellationToken cancellationToken)
    {
        var command = UpdateAlertCommandFromResourceAssembler.ToCommandFromResource(resource, alertId);
        var result  = await alertCommandService.Handle(command, cancellationToken);
        return MonitoringActionResultAssembler.ToActionResultFromUpdateAlertResult(
            this, result, _errorLocalizer, _problemDetailsFactory,
            updated => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}