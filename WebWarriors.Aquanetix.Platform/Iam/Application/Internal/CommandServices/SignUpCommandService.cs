using Microsoft.AspNetCore.Identity;
using WebWarriors.Aquanetix.Platform.Iam.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Iam.Application.Internal.OutboundServices;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Iam.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Interfaces.Acl;

namespace WebWarriors.Aquanetix.Platform.Iam.Application.Internal.CommandServices;

/// <summary>
///     User registration. Validates the plan, creates the user (hashed password),
///     creates its subscription (IAM→Subscription link), and returns the
///     authenticated user with a JWT (auto-login after sign-up).
/// </summary>
public class SignUpCommandService(
    IUserRepository userRepository,
    PasswordHasher<User> passwordHasher,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    ISubscriptionContextFacade subscriptionContextFacade)
    : ISignUpCommandService
{
    public async Task<Result<AuthenticatedUser>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            return Result<AuthenticatedUser>.Failure(IamError.InvalidCredentials, "Email and password are required");

        // FIX: validate the plan BEFORE creating the user (avoids orphan users with invalid plans).
        if (!subscriptionContextFacade.IsValidPlan(command.Plan))
            return Result<AuthenticatedUser>.Failure(IamError.InvalidCredentials,
                "Invalid plan. Choose Basic, Smart City or Industrial");

        // Email must be unique.
        var existing = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (existing is not null)
            return Result<AuthenticatedUser>.Failure(IamError.InvalidCredentials, "A user with that email already exists");

        try
        {
            var user = new User(command.Email, string.Empty);
            var hashed = passwordHasher.HashPassword(user, command.Password);
            user = new User(command.Email, hashed);

            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            // IAM → Subscription link: create the subscription with the chosen plan.
            await subscriptionContextFacade.CreateSubscriptionForUser(user.Id, command.Plan);

            // Auto-login: issue a JWT so the user is authenticated right after signing up.
            var token = tokenService.GenerateToken(user);
            return Result<AuthenticatedUser>.Success(new AuthenticatedUser(user.Id, user.Email, user.Role, token));
        }
        catch (OperationCanceledException)
        {
            return Result<AuthenticatedUser>.Failure(IamError.OperationCancelled, "Operation cancelled");
        }
        catch (Exception)
        {
            return Result<AuthenticatedUser>.Failure(IamError.InvalidCredentials, "Could not create the user");
        }
    }
}
