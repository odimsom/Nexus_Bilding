using MediatR;

namespace NexusBilling.Core.Application.Inventory.Features.Locations.Commands.UpsertLocation;

public record UpsertLocationResult(string Code, bool Created);

public record UpsertLocationCommand(
    Guid TenantId,
    string? OriginalCode,
    string Code,
    string Name,
    string Address,
    string City
) : IRequest<UpsertLocationResult>;
