using MediatR;
using NexusBilling.Core.Application.Inventory.DTOs;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItemByNo;

public sealed class GetItemByNoQueryHandler(IItemRepository repo)
    : IRequestHandler<GetItemByNoQuery, ItemDto?>
{
    public async Task<ItemDto?> Handle(GetItemByNoQuery request, CancellationToken cancellationToken)
    {
        var item = await repo.GetByNoForTenantAsync(request.TenantId, request.No, cancellationToken);
        if (item is null) return null;

        var stock = await repo.GetInventoryForItemAsync(request.TenantId, request.No, cancellationToken);

        return new ItemDto(
            item.No, item.Description, item.Description2, item.BaseUnitOfMeasure,
            item.UnitPrice, item.UnitCost, item.Blocked, stock,
            item.Type, item.ItemCategoryCode, item.InventoryPostingGroup,
            item.GenProdPostingGroup, item.StandardCost, item.LastDirectCost);
    }
}
