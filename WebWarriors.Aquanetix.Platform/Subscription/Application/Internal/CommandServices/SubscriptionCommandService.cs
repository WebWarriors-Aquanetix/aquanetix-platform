using WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Subscription.Domain.Services;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using SubscriptionEntity = WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription;

namespace WebWarriors.Aquanetix.Platform.Subscription.Application.Internal.CommandServices;

public class SubscriptionCommandService
    : ISubscriptionCommandService
{
    private readonly ISubscriptionRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public SubscriptionCommandService(
        ISubscriptionRepository repository,
        IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async  Task<WebWarriors.Aquanetix.Platform.Subscription.Domain.Model.Aggregates.Subscription?> Handle(
        CreateSubscriptionCommand command)
    {
        var subscription = new SubscriptionEntity(
            command.UserId,
            command.Plan,
            command.Status
        );

        await repository.AddAsync(subscription);

        await unitOfWork.CompleteAsync();

        return subscription;
    }
    public async Task<SubscriptionEntity?> Handle(
        CancelSubscriptionCommand command)
    {
        var subscription =
            await repository.FindByIdAsync(command.Id);

        if (subscription is null)
            return null;

        subscription.Cancel();

        repository.Update(subscription);

        await unitOfWork.CompleteAsync();

        return subscription;
    }
    public async Task<SubscriptionEntity?> Handle(
        RenewSubscriptionCommand command)
    {
        var subscription =
            await repository.FindByIdAsync(command.Id);

        if (subscription is null)
            return null;

        subscription.Renew();

        repository.Update(subscription);

        await unitOfWork.CompleteAsync();

        return subscription;
    }
}
