using WebWarriors.Aquanetix.Platform.Iam.Application.Internal.OutboundServices;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Validates the JWT for endpoints marked with [Authorize].
///     - [AllowAnonymous] endpoints are always allowed.
///     - Endpoints without [Authorize] are allowed (opt-in protection).
///     - Endpoints with [Authorize] require a valid "Authorization: Bearer {token}".
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        var endpoint = context.GetEndpoint();

        // Public if explicitly [AllowAnonymous].
        var allowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() is not null;
        // Protected only if [Authorize] is present.
        var requiresAuth = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() is not null;

        if (allowAnonymous || !requiresAuth)
        {
            await next(context);
            return;
        }

        // Extract "Authorization: Bearer {token}".
        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
        var token = authHeader?.StartsWith("Bearer ") == true
            ? authHeader["Bearer ".Length..].Trim()
            : null;

        var userId = token is not null ? await tokenService.ValidateToken(token) : null;
        if (userId is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = "Unauthorized: missing or invalid token" });
            return;
        }

        // Attach the user to the context (optional, useful downstream).
        var user = await userRepository.FindByIdAsync(userId.Value);
        if (user is not null)
            context.Items["User"] = user;

        await next(context);
    }
}
