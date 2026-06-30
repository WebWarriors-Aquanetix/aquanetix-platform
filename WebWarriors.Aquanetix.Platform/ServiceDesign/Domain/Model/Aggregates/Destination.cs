using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Domain.Model.Entities;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root representing a registered destination site
///     (e.g. "Planta Norte") within the Service Design and Planning
///     Bounded Context. Water batches are delivered to a destination,
///     and devices are installed at a destination.
/// </summary>
public class Destination : IAuditableEntity
{
    public Destination() { }

    public Destination(CreateDestinationCommand command) : this()
    {
        Name        = command.Name;
        Address     = command.Address;
        Description = command.Description;
    }

    public int    Id          { get; private set; }
    public string Name        { get; private set; } = string.Empty;
    public string Address     { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
