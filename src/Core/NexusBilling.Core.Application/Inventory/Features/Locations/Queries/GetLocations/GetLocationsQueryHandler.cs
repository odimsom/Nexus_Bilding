using MediatR;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocations;

public sealed class GetLocationsQueryHandler(ILocationRepository repo)
    : IRequestHandler<GetLocationsQuery, List<LocationDto>>
{
    public async Task<List<LocationDto>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await repo.FindAsync(l => l.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(request.TenantId), null, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var s = request.Search.ToLowerInvariant();
            locations = locations.Where(l => 
                l.Code.ToLowerInvariant().Contains(s) || 
                l.Name.ToLowerInvariant().Contains(s))
                .ToList();
        }

        return locations.Select(l => new LocationDto(l.Code, l.Name, l.Address, l.City)).ToList();
    }
}
