using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.Iam.Application.CommandServices;

public interface ISignUpCommandService
{
    /// <summary>Creates a user + its subscription, and returns the authenticated user (with JWT).</summary>
    Task<Result<AuthenticatedUser>> Handle(SignUpCommand command, CancellationToken cancellationToken);
}
