using WebWarriors.Aquanetix.Platform.Devices.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Acl;

/// <summary>
///     Implementation of ServiceDesign's ACL over Devices. It depends only on the
///     public Devices façade contract (IDevicesContextFacade), never on Devices internals.
/// </summary>
public class ExternalDevicesService(IDevicesContextFacade devicesContextFacade) : IExternalDevicesService
{
    /// <inheritdoc />
    public async Task<bool> IsDestinationUsedByDevice(int destinationId, CancellationToken cancellationToken)
        => await devicesContextFacade.ExistsDeviceAtDestination(destinationId, cancellationToken);
}
