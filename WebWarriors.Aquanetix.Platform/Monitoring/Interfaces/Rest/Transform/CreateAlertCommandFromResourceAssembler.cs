using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Transform;

public static class CreateAlertCommandFromResourceAssembler
{
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource) =>
        new(resource.DeviceId,
            resource.DeviceName,
            resource.Location,
            resource.Type,
            resource.Severity,
            resource.Message,
            resource.Timestamp,
            resource.Status,
            resource.Value,
            resource.Threshold);
}