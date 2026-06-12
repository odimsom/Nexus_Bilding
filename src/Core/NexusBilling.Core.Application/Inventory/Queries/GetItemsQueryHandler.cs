using MediatR;
using NexusBilling.Core.Application.Inventory.DTOs;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Queries;

public sealed class GetItemsQueryHandler(IItemRepository repo)
    : IRequestHandler<GetItemsQuery, GetItemsResult>
{
    public async Task<GetItemsResult> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var ps = Math.Clamp(request.PageSize, 1, 200);
        var pg = Math.Max(1, request.Page);

        var (items, total) = await repo.ListAsync(
            request.TenantId, request.Search, request.Blocked, pg, ps, cancellationToken);

        var totalPages = (int)Math.Ceiling(total / (double)ps);

        var itemNos = items.Select(i => i.No).ToList();
        var inventory = await repo.GetInventoryByItemNosAsync(request.TenantId, itemNos, cancellationToken);

        var dtos = items.Select(i => new ItemListDto(
            i.No, i.Description, i.BaseUnitOfMeasure, i.UnitPrice, i.UnitCost, i.Blocked,
            inventory.TryGetValue(i.No, out var stock) ? stock : 0m,
            i.Type, i.ItemCategoryCode))
            .ToList();

        return new GetItemsResult(dtos, total, totalPages);
    }
}
