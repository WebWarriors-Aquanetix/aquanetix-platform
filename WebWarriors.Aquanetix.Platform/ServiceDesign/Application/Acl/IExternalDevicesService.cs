namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Acl;

/// <summary>
///     ServiceDesign's anti-corruption layer over the Devices context.
///     Translates the Devices façade into ServiceDesign's own language so the
///     rest of the context never depends on Devices types directly.
/// </summary>
public interface IExternalDevicesService
{
    /// <summary>True if any device is installed at the given destination.</summary>
    Task<bool> IsDestinationUsedByDevice(int destinationId, CancellationToken cancellationToken);
}
