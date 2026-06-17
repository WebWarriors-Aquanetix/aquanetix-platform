using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Application.CommandServices;

public interface IQualityAnalysisCommandService
{
    Task<Result<QualityAnalysis>> Handle(CreateQualityAnalysisCommand command, CancellationToken cancellationToken);
}
