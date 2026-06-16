using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Resources;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST.Transform;

namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class DevicesController(IDeviceQueryService deviceQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllDevices(CancellationToken cancellationToken)
    {
        var query = new GetAllDevicesQuery();
        var devices = await deviceQueryService.Handle(query, cancellationToken);
        
        var resources = devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }
}