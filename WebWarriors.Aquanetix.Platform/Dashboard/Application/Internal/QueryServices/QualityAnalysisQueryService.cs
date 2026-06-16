using WebWarriors.Aquanetix.Platform.Dashboard.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Application.Internal.QueryServices;

public class QualityAnalysisQueryService(IQualityAnalysisRepository qualityAnalysisRepository)
    : IQualityAnalysisQueryService
{
    public async Task<QualityAnalysis?> Handle(GetQualityAnalysisByIdQuery query,
        CancellationToken cancellationToken)
        => await qualityAnalysisRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<QualityAnalysis>> Handle(GetAllQualityAnalysesQuery query,
        CancellationToken cancellationToken)
        => await qualityAnalysisRepository.ListAsync(cancellationToken);
}