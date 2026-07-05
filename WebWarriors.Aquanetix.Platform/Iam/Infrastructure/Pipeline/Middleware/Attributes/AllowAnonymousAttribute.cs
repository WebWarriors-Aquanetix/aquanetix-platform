namespace WebWarriors.Aquanetix.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Marks a controller or action as public (skips authorization),
///     even if [Authorize] is applied elsewhere.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}
