using MediatR;
using System.Collections.Generic;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocations;

public record LocationDto(string Code, string Name, string Address, string City);

public record GetLocationsQuery(Guid TenantId, string? Search = null) : IRequest<List<LocationDto>>;
