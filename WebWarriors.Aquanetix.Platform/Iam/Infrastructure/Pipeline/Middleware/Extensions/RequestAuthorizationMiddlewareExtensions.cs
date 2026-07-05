using WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    /// <summary>Adds the JWT authorization middleware to the pipeline.</summary>
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
