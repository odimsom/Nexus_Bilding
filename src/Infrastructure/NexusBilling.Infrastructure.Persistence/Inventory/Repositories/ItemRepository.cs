using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Repositories;

public class ItemRepository(NexusBillingDbContext dbContext)
    : GenericRepository<Item>(dbContext), IItemRepository
{
    public async Task<Item?> GetByNoAsync(string no, CancellationToken ct = default)
        => await _dbContext.Items.FirstOrDefaultAsync(x => x.No == no, ct);

    public async Task<Item?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        return await _dbContext.Items
            .FirstOrDefaultAsync(x => x.TenantId == tid && x.No == no, ct);
    }

    public async Task<(IReadOnlyList<Item> Items, int Total)> ListAsync(
        Guid tenantId, string? search, bool? blocked, int page, int pageSize,
        CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        var query = _dbContext.Items.Where(i => i.TenantId == tid);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.ToLower();
            query = query.Where(i =>
                i.No.ToLower().Contains(q) ||
                i.Description.ToLower().Contains(q));
        }

        if (blocked.HasValue)
            query = query.Where(i => i.Blocked == blocked.Value);

        var total = await query.CountAsync(ct);
        var ps = Math.Clamp(pageSize, 1, 200);
        var pg = Math.Max(1, page);

        var items = (IReadOnlyList<Item>)await query
            .OrderBy(i => i.No)
            .Skip((pg - 1) * ps)
            .Take(ps)
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task<Dictionary<string, decimal>> GetInventoryByItemNosAsync(
        Guid tenantId, IReadOnlyList<string> itemNos, CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        return await _dbContext.ItemLedgerEntries
            .Where(e => e.TenantId == tid && itemNos.Contains(e.ItemNo!))
            .GroupBy(e => e.ItemNo!)
            .Select(g => new { ItemNo = g.Key, Stock = g.Sum(e => e.Quantity) })
            .ToDictionaryAsync(x => x.ItemNo, x => x.Stock, ct);
    }

    public async Task<decimal> GetInventoryForItemAsync(
        Guid tenantId, string itemNo, CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        return await _dbContext.ItemLedgerEntries
            .Where(e => e.TenantId == tid && e.ItemNo == itemNo)
            .SumAsync(e => e.Quantity, ct);
    }
}
