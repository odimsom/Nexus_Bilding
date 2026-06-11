using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class SalesHeaderRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<SalesHeader>(dbContext), ISalesHeaderRepository
{
    public async Task<SalesHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<SalesHeader>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
