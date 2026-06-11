using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class VendorRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<Vendor>(dbContext), IVendorRepository
{
    public async Task<Vendor?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Vendor>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
