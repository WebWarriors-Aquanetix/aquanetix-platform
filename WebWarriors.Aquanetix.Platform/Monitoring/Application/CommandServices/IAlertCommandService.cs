using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Application.CommandServices;

public interface IAlertCommandService
{
    Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken);
}