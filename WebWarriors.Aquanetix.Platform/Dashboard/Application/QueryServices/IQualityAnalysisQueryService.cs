using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Queries;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Application.QueryServices;

public interface IQualityAnalysisQueryService
{
    Task<QualityAnalysis?> Handle(GetQualityAnalysisByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<QualityAnalysis>> Handle(GetAllQualityAnalysesQuery query, CancellationToken cancellationToken);
}