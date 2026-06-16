using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebWarriors.Aquanetix.Platform.Devices.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Internal.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IDeviceCommandService
{
    /// <inheritdoc />
    public async Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = new Device(command.OwnerId, command.SerialNumber, command.DeviceType);
        try
        {
            await deviceRepository.AddAsync(device, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Device>.Success(device);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(DevicesError.OperationCancelled,
                localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Device>> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindByIdAsync(command.Id, cancellationToken);
        if (device is null)
            return Result<Device>.Failure(DevicesError.DeviceNotFound,
                localizer[nameof(DevicesError.DeviceNotFound)]);
        try
        {
            device.Update(command);
            deviceRepository.Update(device);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Device>.Success(device);
        }
        catch (OperationCanceledException)
        {
            return Result<Device>.Failure(DevicesError.OperationCancelled,
                localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Device>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
        catch (Exception)
        {
            return Result<Device>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
    }
}
