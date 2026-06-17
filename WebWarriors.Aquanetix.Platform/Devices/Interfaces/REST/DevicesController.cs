using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebWarriors.Aquanetix.Platform.Devices.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Device endpoints")]
public class DevicesController(
    IDeviceQueryService deviceQueryService,
    IDeviceCommandService deviceCommandService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all devices", OperationId = "GetAllDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "Devices retrieved", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
    {
        var devices = await deviceQueryService.Handle(new GetAllDevicesQuery(), cancellationToken);
        return Ok(devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{deviceId:int}")]
    [SwaggerOperation(Summary = "Get device by id", OperationId = "GetDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Device found", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Device not found")]
    public async Task<IActionResult> GetDeviceById([FromRoute] int deviceId, CancellationToken cancellationToken)
    {
        var device = await deviceQueryService.Handle(new GetDeviceByIdQuery(deviceId), cancellationToken);
        if (device is null)
            return NotFound(new { message = $"Device with id {deviceId} not found" });
        return Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(device));
    }

    [HttpPut("{deviceId:int}")]
    [SwaggerOperation(Summary = "Update device monitoring frequency", OperationId = "UpdateDevice")]
    [SwaggerResponse(StatusCodes.Status200OK, "Device updated", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Device not found")]
    public async Task<IActionResult> UpdateDevice([FromRoute] int deviceId,
        [FromBody] UpdateDeviceResource resource, CancellationToken cancellationToken)
    {
        var command = UpdateDeviceCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var result  = await deviceCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(new { message = result.Message });
        return Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new device", OperationId = "CreateDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "Device created", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid device data")]
    public async Task<IActionResult> CreateDevice(
        [FromBody] CreateDeviceResource resource, CancellationToken cancellationToken)
    {
        var command = CreateDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result  = await deviceCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Message });
        var created = DeviceResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = created.Id }, created);
    }

    [HttpGet("{deviceId:int}/thresholds")]
    [SwaggerOperation(Summary = "Get thresholds by device id", OperationId = "GetThresholdsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Thresholds retrieved", typeof(IEnumerable<ThresholdResource>))]
    public async Task<IActionResult> GetThresholdsByDeviceId([FromRoute] int deviceId, CancellationToken cancellationToken)
    {
        var thresholds = await deviceQueryService.Handle(new GetThresholdsByDeviceIdQuery(deviceId), cancellationToken);
        return Ok(thresholds.Select(ThresholdResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost("{deviceId:int}/thresholds")]
    [SwaggerOperation(Summary = "Create threshold for device", OperationId = "CreateThreshold")]
    [SwaggerResponse(StatusCodes.Status201Created, "Threshold created", typeof(ThresholdResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Device not found")]
    public async Task<IActionResult> CreateThreshold([FromRoute] int deviceId,
        [FromBody] CreateThresholdResource resource, CancellationToken cancellationToken)
    {
        var command = CreateThresholdCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var result  = await deviceCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(new { message = result.Message });
        return CreatedAtAction(nameof(GetThresholdsByDeviceId),
            new { deviceId },
            ThresholdResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
