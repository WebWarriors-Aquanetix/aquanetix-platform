using WebWarriors.Aquanetix.Platform.Monitoring.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Application.Internal.QueryServices;

public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    public async Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken)
        => await alertRepository.FindByIdAsync(query.AlertId, cancellationToken);

    public async Task<IEnumerable<Alert>> Handle(GetAlertsByDeviceIdQuery query, CancellationToken cancellationToken)
        => await alertRepository.FindByDeviceIdAsync(query.DeviceId, cancellationToken);
}