using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Acl;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Application.Acl;

/// <summary>
///     Implementation of the Monitoring context façade. Uses Monitoring's own
///     repository, so the dependency on Monitoring internals stays inside Monitoring.
/// </summary>
public class MonitoringContextFacade(
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork) : IMonitoringContextFacade
{
    /// <inheritdoc />
    public async Task<int> DeleteAlertsByDeviceId(int deviceId, CancellationToken cancellationToken)
    {
        var alerts = (await alertRepository.FindByDeviceIdAsync(deviceId, cancellationToken)).ToList();
        if (alerts.Count == 0) return 0;

        foreach (var alert in alerts)
            alertRepository.Remove(alert);

        await unitOfWork.CompleteAsync(cancellationToken);
        return alerts.Count;
    }
}
