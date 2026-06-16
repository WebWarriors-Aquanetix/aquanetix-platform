using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.ValueObjects;
using WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest.Transform;

public static class CreateQualityAnalysisCommandFromResourceAssembler
{
    public static CreateQualityAnalysisCommand ToCommandFromResource(CreateQualityAnalysisResource resource) =>
        new(resource.SensorSourceId,
            Enum.Parse<AnomalyType>(resource.DetectedParameters),
            resource.SeverityScore);
}
