using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class CustomerRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<Customer>(dbContext), ICustomerRepository
{
    public async Task<Customer?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Customer>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
