using WebWarriors.Aquanetix.Platform.ServiceDesign.Application.QueryServices;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Aggregates;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Model.Queries;
using WebWarriors.Aquanetix.Platform.ServiceDesign.Domain.Repositories;

namespace WebWarriors.Aquanetix.Platform.ServiceDesign.Application.Internal.QueryServices;

public class DestinationQueryService(IDestinationRepository destinationRepository) : IDestinationQueryService
{
    public async Task<IEnumerable<Destination>> Handle(GetAllDestinationsQuery query, CancellationToken cancellationToken)
        => await destinationRepository.ListAsync(cancellationToken);

    public async Task<Destination?> Handle(GetDestinationByIdQuery query, CancellationToken cancellationToken)
        => await destinationRepository.FindByIdAsync(query.DestinationId, cancellationToken);
}
