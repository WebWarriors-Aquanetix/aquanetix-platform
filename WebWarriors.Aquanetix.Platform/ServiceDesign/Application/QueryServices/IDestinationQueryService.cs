using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Queries;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.QueryServices;

public interface IDestinationQueryService
{
    Task<IEnumerable<Destination>> Handle(GetAllDestinationsQuery query, CancellationToken cancellationToken);
    Task<Destination?> Handle(GetDestinationByIdQuery query, CancellationToken cancellationToken);
}
