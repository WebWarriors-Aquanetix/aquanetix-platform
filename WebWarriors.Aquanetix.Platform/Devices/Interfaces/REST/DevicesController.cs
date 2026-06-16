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
}
