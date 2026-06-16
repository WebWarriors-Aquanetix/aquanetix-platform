using WebWarriors.Aquanetix.Platform.Monitoring.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Resources;

namespace WebWarriors.Aquanetix.Platform.Monitoring.Interfaces.Rest.Transform;

public static class UpdateAlertCommandFromResourceAssembler
{
    public static UpdateAlertCommand ToCommandFromResource(UpdateAlertResource resource, int alertId) =>
        new(alertId,
            resource.DeviceId,
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