namespace WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest.Resources;

public record CreateQualityAnalysisResource(
    int    SensorSourceId,
    string DetectedParameters,
    double SeverityScore);
