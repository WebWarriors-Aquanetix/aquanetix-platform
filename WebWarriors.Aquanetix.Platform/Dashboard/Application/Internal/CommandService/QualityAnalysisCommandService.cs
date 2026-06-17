using Microsoft.EntityFrameworkCore;
using WebWarriors.Aquanetix.Platform.Dashboard.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Application.Internal.CommandServices;

public class QualityAnalysisCommandService(
    IQualityAnalysisRepository qualityAnalysisRepository,
    IUnitOfWork unitOfWork)
    : IQualityAnalysisCommandService
{
    public async Task<Result<QualityAnalysis>> Handle(CreateQualityAnalysisCommand command,
        CancellationToken cancellationToken)
    {
        var analysis = new QualityAnalysis(
            command.SensorSourceId,
            command.DetectedParameters,
            command.SeverityScore);
        try
        {
            await qualityAnalysisRepository.AddAsync(analysis, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<QualityAnalysis>.Success(analysis);
        }
        catch (DbUpdateException ex)
        {
            return Result<QualityAnalysis>.Failure(null, ex.Message);
        }
        catch (Exception ex)
        {
            return Result<QualityAnalysis>.Failure(null, ex.Message);
        }
    }
}
