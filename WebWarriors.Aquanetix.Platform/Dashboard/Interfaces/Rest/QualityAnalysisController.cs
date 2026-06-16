using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebWarriors.Aquanetix.Platform.Dashboard.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.Dashboard.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest.Resources;
using WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest.Transform;

namespace WebWarriors.Aquanetix.Platform.Dashboard.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Quality Analysis endpoints")]
public class QualityAnalysisController(IQualityAnalysisQueryService qualityAnalysisQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all quality analyses", OperationId = "GetAllQualityAnalyses")]
    [SwaggerResponse(StatusCodes.Status200OK, "Quality analyses retrieved", typeof(IEnumerable<QualityAnalysisResource>))]
    public async Task<IActionResult> GetAllQualityAnalyses(CancellationToken cancellationToken)
    {
        var analyses = await qualityAnalysisQueryService.Handle(new GetAllQualityAnalysesQuery(), cancellationToken);
        return Ok(analyses.Select(QualityAnalysisResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get quality analysis by id", OperationId = "GetQualityAnalysisById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Quality analysis found", typeof(QualityAnalysisResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Quality analysis not found")]
    public async Task<IActionResult> GetQualityAnalysisById(int id, CancellationToken cancellationToken)
    {
        var result = await qualityAnalysisQueryService.Handle(new GetQualityAnalysisByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(QualityAnalysisResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}