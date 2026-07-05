using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Resources;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest.Transform;
using WebWarriors.Aquanetix.Platform.Shared.Interfaces.Rest.ProblemDetails;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Destination endpoints")]
[AllowAnonymous]     
public class DestinationsController(
    IDestinationQueryService destinationQueryService,
    IDestinationCommandService destinationCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all destinations", OperationId = "GetAllDestinations")]
    [SwaggerResponse(StatusCodes.Status200OK, "Destinations retrieved", typeof(IEnumerable<DestinationResource>))]
    public async Task<IActionResult> GetAllDestinations(CancellationToken cancellationToken)
    {
        var destinations = await destinationQueryService.Handle(new GetAllDestinationsQuery(), cancellationToken);
        return Ok(destinations.Select(DestinationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{destinationId:int}")]
    [SwaggerOperation(Summary = "Get destination by id", OperationId = "GetDestinationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Destination found", typeof(DestinationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Destination not found")]
    public async Task<IActionResult> GetDestinationById([FromRoute] int destinationId, CancellationToken cancellationToken)
    {
        var destination = await destinationQueryService.Handle(new GetDestinationByIdQuery(destinationId), cancellationToken);
        if (destination is null)
            return problemDetailsFactory.CreateProblemDetails(
                this, StatusCodes.Status404NotFound, "DestinationNotFound",
                errorLocalizer["ServiceDesignError.DestinationNotFound"]);
        return Ok(DestinationResourceFromEntityAssembler.ToResourceFromEntity(destination));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a destination", OperationId = "CreateDestination")]
    [SwaggerResponse(StatusCodes.Status201Created, "Destination created", typeof(DestinationResource))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "A destination with that name already exists")]
    public async Task<IActionResult> CreateDestination([FromBody] CreateDestinationResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateDestinationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result  = await destinationCommandService.Handle(command, cancellationToken);
        if (!result.IsSuccess)
        {
            var status = Equals(result.Error, ServiceDesignError.DestinationNameAlreadyExists)
                ? StatusCodes.Status409Conflict
                : StatusCodes.Status400BadRequest;
            return problemDetailsFactory.CreateProblemDetails(this, status, result.Error?.ToString(), result.Message);
        }
        return CreatedAtAction(nameof(GetDestinationById),
            new { destinationId = result.Value!.Id },
            DestinationResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpDelete("{destinationId:int}")]
    [SwaggerOperation(Summary = "Delete a destination", OperationId = "DeleteDestination")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Destination deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Destination not found")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Destination is in use and cannot be deleted")]
    public async Task<IActionResult> DeleteDestination([FromRoute] int destinationId, CancellationToken cancellationToken)
    {
        var result = await destinationCommandService.Handle(new DeleteDestinationCommand(destinationId), cancellationToken);
        if (!result.IsSuccess)
        {
            var status = Equals(result.Error, ServiceDesignError.DestinationInUse)
                ? StatusCodes.Status409Conflict
                : StatusCodes.Status404NotFound;
            return problemDetailsFactory.CreateProblemDetails(this, status, result.Error?.ToString(), result.Message);
        }
        return NoContent();
    }
}
