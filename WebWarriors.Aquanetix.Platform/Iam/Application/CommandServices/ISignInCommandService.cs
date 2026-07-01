using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.Iam.Application.CommandServices;

public interface ISignInCommandService
{
    Task<Result<AuthenticatedUser>> Handle(SignInCommand command, CancellationToken cancellationToken);
}
