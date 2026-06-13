using MediatR;
using NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocations;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Queries.GetLocationByCode;

public record GetLocationByCodeQuery(Guid TenantId, string Code) : IRequest<LocationDto?>;
