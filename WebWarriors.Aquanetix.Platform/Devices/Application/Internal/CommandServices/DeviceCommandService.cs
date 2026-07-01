using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebWarriors.Aquanetix.Platform.Devices.Application.CommandServices;
using WebWarriors.Aquanetix.Platform.Devices.Application.Acl;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Command;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Model.Entities;
using WebWarriors.Aquanetix.Platform.Devices.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Repositories;
using WebWarriors.Aquanetix.Platform.Shared.Resources.Errors;

namespace WebWarriors.Aquanetix.Platform.Devices.Application.Internal.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository,
    IExternalMonitoringService externalMonitoringService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IDeviceCommandService
{
    public async Task<Result<Device>> Handle(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = new Device(
            command.OwnerId,
            command.SerialNumber,
            command.DeviceType,
            command.Name ?? string.Empty,
            command.Location ?? string.Empty,
            command.Unit ?? string.Empty,
            command.CurrentValue ?? 0d,
            command.DestinationId);
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

    public async Task<Result<ThresholdConfiguration>> Handle(CreateThresholdCommand command, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId, cancellationToken);
        if (device is null)
            return Result<ThresholdConfiguration>.Failure(DevicesError.DeviceNotFound,
                localizer[nameof(DevicesError.DeviceNotFound)]);
        try
        {
            var threshold = new ThresholdConfiguration(
                command.DeviceId,
                command.MinValue,
                command.MaxValue,
                command.Unit,
                command.AlertLevel);
            device.AddThreshold(threshold);
            deviceRepository.Update(device);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ThresholdConfiguration>.Success(threshold);
        }
        catch (OperationCanceledException)
        {
            return Result<ThresholdConfiguration>.Failure(DevicesError.OperationCancelled,
                localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<ThresholdConfiguration>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
        catch (Exception)
        {
            return Result<ThresholdConfiguration>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
    }

    public async Task<Result<bool>> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = await deviceRepository.FindByIdAsync(command.Id, cancellationToken);
        if (device is null)
            return Result<bool>.Failure(DevicesError.DeviceNotFound,
                localizer[nameof(DevicesError.DeviceNotFound)]);
        try
        {
            // Cascade 1: alerts live in the Monitoring bounded context.
            // Delete them through the ACL, never touching Monitoring tables directly.
            await externalMonitoringService.DeleteAlertsForDevice(command.Id, cancellationToken);

            // Cascade 2: thresholds live inside Devices and are removed by EF Core
            // cascade delete (FK_threshold_device) when the device is removed.
            deviceRepository.Remove(device);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(DevicesError.OperationCancelled,
                localizer[nameof(DevicesError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
        catch (Exception)
        {
            return Result<bool>.Failure(DevicesError.InvalidDeviceData,
                localizer[nameof(DevicesError.InvalidDeviceData)]);
        }
    }
}
