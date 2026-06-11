using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class CustLedgerEntryRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<CustLedgerEntry>(dbContext), ICustLedgerEntryRepository
{
    public async Task<CustLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CustLedgerEntry>()
            .FirstOrDefaultAsync(x => x.EntryNo == entryNo, cancellationToken);
    }
}
