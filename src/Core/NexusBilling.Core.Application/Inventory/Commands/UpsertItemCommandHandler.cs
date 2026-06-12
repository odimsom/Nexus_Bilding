using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Commands;

public sealed class UpsertItemCommandHandler(IItemRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpsertItemCommand, UpsertItemResult>
{
    public async Task<UpsertItemResult> Handle(UpsertItemCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);
        var lookupNo = cmd.ExistingNo ?? cmd.No;
        var existing = await repo.GetByNoForTenantAsync(cmd.TenantId, lookupNo, ct);

        if (existing is not null)
        {
            existing.Description = cmd.Description;
            existing.Description2 = cmd.Description2;
            existing.BaseUnitOfMeasure = cmd.BaseUnitOfMeasure;
            existing.UnitPrice = cmd.UnitPrice;
            existing.UnitCost = cmd.UnitCost;
            existing.StandardCost = cmd.StandardCost;
            existing.Type = cmd.Type;
            existing.ItemCategoryCode = cmd.ItemCategoryCode;
            existing.InventoryPostingGroup = cmd.InventoryPostingGroup;
            existing.GenProdPostingGroup = cmd.GenProdPostingGroup;
            existing.VatProdPostingGroup = cmd.VatProdPostingGroup;
            existing.VendorNo = cmd.VendorNo;
            existing.VendorItemNo = cmd.VendorItemNo;
            await repo.UpdateAsync(existing, ct);
            await uow.SaveChangesAsync(ct);
            return new UpsertItemResult(false, existing.No);
        }

        var result = Domain.Inventory.Entities.Item.Create(
            tid, cmd.No, cmd.Description, cmd.BaseUnitOfMeasure, cmd.UnitPrice, cmd.UnitCost, cmd.Type);

        if (!result.IsSuccess)
            throw new InvalidOperationException(result.GetError()!.Message);

        var item = result.GetValue()!;
        item.Description2 = cmd.Description2;
        item.StandardCost = cmd.StandardCost;
        item.ItemCategoryCode = cmd.ItemCategoryCode;
        item.InventoryPostingGroup = cmd.InventoryPostingGroup;
        item.GenProdPostingGroup = cmd.GenProdPostingGroup;
        item.VatProdPostingGroup = cmd.VatProdPostingGroup;
        item.VendorNo = cmd.VendorNo;
        item.VendorItemNo = cmd.VendorItemNo;

        await repo.AddAsync(item, ct);
        await uow.SaveChangesAsync(ct);
        return new UpsertItemResult(true, item.No);
    }
}
