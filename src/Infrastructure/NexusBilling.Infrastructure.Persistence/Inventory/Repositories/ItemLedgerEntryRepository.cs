using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Repositories;

public class ItemLedgerEntryRepository(NexusBillingDbContext dbContext)
    : GenericRepository<ItemLedgerEntry>(dbContext), IItemLedgerEntryRepository
{
    public async Task<ItemLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default)
        => await _dbContext.Set<ItemLedgerEntry>()
            .FirstOrDefaultAsync(x => x.EntryNo == entryNo, cancellationToken);

    public async Task<IReadOnlyList<ItemLedgerEntry>> GetForItemAsync(Guid tenantId, string itemNo, int page, int pageSize, CancellationToken ct = default)
        => await _dbContext.Set<ItemLedgerEntry>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.ItemNo == itemNo)
            .OrderByDescending(x => x.PostingDate)
            .ThenByDescending(x => x.EntryNo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

    public async Task<int> GetTotalForItemAsync(Guid tenantId, string itemNo, CancellationToken ct = default)
        => await _dbContext.Set<ItemLedgerEntry>()
            .CountAsync(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.ItemNo == itemNo, ct);

    public async Task<int> GetNextEntryNoAsync(Guid tenantId, CancellationToken ct = default)
    {
        var max = await _dbContext.Set<ItemLedgerEntry>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId))
            .MaxAsync(x => (int?)x.EntryNo, ct);
        return (max ?? 0) + 1;
    }
}
