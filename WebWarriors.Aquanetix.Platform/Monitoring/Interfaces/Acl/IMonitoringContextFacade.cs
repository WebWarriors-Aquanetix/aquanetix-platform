namespace WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Acl;

/// <summary>
///     Public façade exposing Monitoring operations to other bounded contexts.
///     Other contexts depend on this contract, never on Monitoring aggregates or tables.
/// </summary>
public interface IMonitoringContextFacade
{
    /// <summary>
    ///     Deletes every alert associated with the given device.
    ///     Used for cascade deletion when a device is removed.
    /// </summary>
    /// <returns>The number of alerts deleted.</returns>
    Task<int> DeleteAlertsByDeviceId(int deviceId, CancellationToken cancellationToken);
}
