using MediatR;
using NexusBilling.Core.Application.Inventory.DTOs;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItems;

public record GetItemsQuery(
    Guid TenantId,
    string? Search,
    bool? Blocked,
    int Page,
    int PageSize
) : IRequest<GetItemsResult>;

public record GetItemsResult(
    IReadOnlyList<ItemListDto> Items,
    int TotalItems,
    int TotalPages
);
