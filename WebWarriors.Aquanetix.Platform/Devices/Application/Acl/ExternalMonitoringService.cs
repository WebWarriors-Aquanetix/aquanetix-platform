using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Acl;

/// <summary>
///     Implementation of Devices' ACL over Monitoring. Depends only on the public
///     Monitoring façade contract (IMonitoringContextFacade), never on internals.
/// </summary>
public class ExternalMonitoringService(IMonitoringContextFacade monitoringContextFacade)
    : IExternalMonitoringService
{
    /// <inheritdoc />
    public async Task<int> DeleteAlertsForDevice(int deviceId, CancellationToken cancellationToken)
        => await monitoringContextFacade.DeleteAlertsByDeviceId(deviceId, cancellationToken);
}
