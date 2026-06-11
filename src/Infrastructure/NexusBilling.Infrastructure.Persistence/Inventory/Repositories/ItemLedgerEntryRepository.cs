using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Repositories;

public class ItemLedgerEntryRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<ItemLedgerEntry>(dbContext), IItemLedgerEntryRepository
{
    public async Task<ItemLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ItemLedgerEntry>()
            .FirstOrDefaultAsync(x => x.EntryNo == entryNo, cancellationToken);
    }
}
