using WebWarriors.Aquanetix.Platform.Devices.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Devices.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Acl;

/// <summary>
///     Implementation of the Devices context façade. It uses the context's own
///     query services, so the dependency on Devices internals stays inside Devices.
/// </summary>
public class DevicesContextFacade(IDeviceQueryService deviceQueryService) : IDevicesContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsDeviceAtDestination(int destinationId, CancellationToken cancellationToken)
    {
        var devices = await deviceQueryService.Handle(new GetAllDevicesQuery(), cancellationToken);
        // After Feature 3, Device exposes DestinationId. This check is forward-compatible.
        return devices.Any(d => d.DestinationId == destinationId);
    }
}
