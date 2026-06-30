using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Commands;
using WebWarriors.Aquanetix.Platform.Shared.Application.Model;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.CommandServices;

public interface IDestinationCommandService
{
    Task<Result<Destination>> Handle(CreateDestinationCommand command, CancellationToken cancellationToken);
    Task<Result<bool>> Handle(DeleteDestinationCommand command, CancellationToken cancellationToken);
}
