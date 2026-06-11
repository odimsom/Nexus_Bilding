using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class PurchaseHeaderRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<PurchaseHeader>(dbContext), IPurchaseHeaderRepository
{
    public async Task<PurchaseHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<PurchaseHeader>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
