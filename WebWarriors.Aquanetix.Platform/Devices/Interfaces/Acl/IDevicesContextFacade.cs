namespace WebWarriors.Aquanetix.Platform.Devices.Interfaces.Acl;

/// <summary>
///     Public façade exposing read-only device information to other bounded contexts.
///     Other contexts depend on this contract, never on the Devices aggregates or tables.
/// </summary>
public interface IDevicesContextFacade
{
    /// <summary>
    ///     Returns true if at least one device is installed at the given destination.
    /// </summary>
    /// <param name="destinationId">The destination id to check.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if any device references the destination, false otherwise.</returns>
    Task<bool> ExistsDeviceAtDestination(int destinationId, CancellationToken cancellationToken);
}
