using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Acl;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Internal.CommandServices;

public class DestinationCommandService(
    IDestinationRepository destinationRepository,
    IExternalDevicesService externalDevicesService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IDestinationCommandService
{
    /// <inheritdoc />
    public async Task<Result<Destination>> Handle(CreateDestinationCommand command, CancellationToken cancellationToken)
    {
        // Business rule: destination name must be unique.
        if (await destinationRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Destination>.Failure(ServiceDesignError.DestinationNameAlreadyExists,
                localizer[nameof(ServiceDesignError.DestinationNameAlreadyExists)]);

        var destination = new Destination(command);
        try
        {
            await destinationRepository.AddAsync(destination, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Destination>.Success(destination);
        }
        catch (OperationCanceledException)
        {
            return Result<Destination>.Failure(ServiceDesignError.OperationCancelled,
                localizer[nameof(ServiceDesignError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Destination>.Failure(ServiceDesignError.DatabaseError,
                localizer[nameof(ServiceDesignError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Destination>.Failure(ServiceDesignError.InternalServerError,
                localizer[nameof(ServiceDesignError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<bool>> Handle(DeleteDestinationCommand command, CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.FindByIdAsync(command.Id, cancellationToken);
        if (destination is null)
            return Result<bool>.Failure(ServiceDesignError.DestinationNotFound,
                localizer[nameof(ServiceDesignError.DestinationNotFound)]);

        // Business rule: cannot delete a destination still in use.
        // - water batches: same bounded context, queried directly.
        // - devices: other bounded context, asked through the ACL (never touching Devices tables).
        var usedByBatch  = await destinationRepository.IsReferencedByWaterBatchAsync(command.Id, cancellationToken);
        var usedByDevice = await externalDevicesService.IsDestinationUsedByDevice(command.Id, cancellationToken);
        if (usedByBatch || usedByDevice)
            return Result<bool>.Failure(ServiceDesignError.DestinationInUse,
                localizer[nameof(ServiceDesignError.DestinationInUse)]);

        try
        {
            destinationRepository.Remove(destination);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(ServiceDesignError.OperationCancelled,
                localizer[nameof(ServiceDesignError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(ServiceDesignError.DatabaseError,
                localizer[nameof(ServiceDesignError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(ServiceDesignError.InternalServerError,
                localizer[nameof(ServiceDesignError.InternalServerError)]);
        }
    }
}
