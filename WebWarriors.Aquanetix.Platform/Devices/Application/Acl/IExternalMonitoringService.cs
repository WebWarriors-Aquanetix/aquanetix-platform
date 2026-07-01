namespace WebWarriors.Aquanetix.Platform.Devices.Application.Acl;

/// <summary>
///     Devices' anti-corruption layer over the Monitoring context.
///     Lets Devices request alert cleanup without depending on Monitoring types.
/// </summary>
public interface IExternalMonitoringService
{
    /// <summary>Deletes all alerts for a device (cascade). Returns count deleted.</summary>
    Task<int> DeleteAlertsForDevice(int deviceId, CancellationToken cancellationToken);
}
