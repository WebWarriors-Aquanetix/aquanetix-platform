using Microsoft.AspNetCore.Identity;
using WebWarriors.Aquanetix.Platform.Iam.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Iam.Application.Internal.OutboundServices;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.Iam.Application.Internal.CommandServices;

public class SignInCommandService(
    IUserRepository userRepository,
    PasswordHasher<User> passwordHasher,
    ITokenService tokenService)
    : ISignInCommandService
{
    public async Task<Result<AuthenticatedUser>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            return InvalidCredentials();

        try
        {
            var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
            if (user is null)
                return InvalidCredentials();

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
                return InvalidCredentials();

            // Generate the JWT for the authenticated user.
            var token = tokenService.GenerateToken(user);
            return Result<AuthenticatedUser>.Success(new AuthenticatedUser(user.Id, user.Email, user.Role, token));
        }
        catch (OperationCanceledException)
        {
            return Result<AuthenticatedUser>.Failure(IamError.OperationCancelled, "Operation cancelled");
        }
    }

    private static Result<AuthenticatedUser> InvalidCredentials()
    {
        return Result<AuthenticatedUser>.Failure(IamError.InvalidCredentials, "Invalid email or password");
    }
}
