using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Queries;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Application.QueryServices;

public interface IAlertQueryService
{
    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> Handle(GetAlertsByDeviceIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> Handle(GetAlertsByStatusQuery query, CancellationToken cancellationToken);
}