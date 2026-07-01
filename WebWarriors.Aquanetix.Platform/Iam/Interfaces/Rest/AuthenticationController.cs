using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebWarriors.Aquanetix.Platform.Iam.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Resources;
using WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest.Transform;

namespace WebWarriors.Aquanetix.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available authentication endpoints")]
public class AuthenticationController(ISignInCommandService signInCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [SwaggerOperation(Summary = "Sign in with email and password", OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "User authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await signInCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return Unauthorized(new { message = result.Message });

        return Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
