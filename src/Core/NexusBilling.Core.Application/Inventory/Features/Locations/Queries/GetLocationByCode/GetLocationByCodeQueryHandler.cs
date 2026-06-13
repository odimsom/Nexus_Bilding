using MediatR;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocations;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocationByCode;

public sealed class GetLocationByCodeQueryHandler(ILocationRepository repo)
    : IRequestHandler<GetLocationByCodeQuery, LocationDto?>
{
    public async Task<LocationDto?> Handle(GetLocationByCodeQuery request, CancellationToken cancellationToken)
    {
        var locations = await repo.FindAsync(l => l.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(request.TenantId) && l.Code == request.Code, null, cancellationToken);
        var location = locations.FirstOrDefault();
        if (location == null) return null;
        
        return new LocationDto(location.Code, location.Name, location.Address, location.City);
    }
}
