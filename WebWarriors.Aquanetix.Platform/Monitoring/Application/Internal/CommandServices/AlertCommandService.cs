using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebWarriors.Aquanetix.Platform.Monitoring.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Application.Internal.CommandServices;

public class AlertCommandService(
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IAlertCommandService
{
    public async Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken)
    {
        var alert = new Alert(command);
        try
        {
            await alertRepository.AddAsync(alert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Alert>.Success(alert);
        }
        catch (OperationCanceledException)
        {
            return Result<Alert>.Failure(MonitoringError.OperationCancelled,
                localizer[nameof(MonitoringError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Alert>.Failure(MonitoringError.DatabaseError,
                localizer[nameof(MonitoringError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Alert>.Failure(MonitoringError.InternalServerError,
                localizer[nameof(MonitoringError.InternalServerError)]);
        }
    }
}