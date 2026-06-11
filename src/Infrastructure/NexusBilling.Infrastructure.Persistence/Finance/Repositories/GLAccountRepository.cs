using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLAccountRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<GLAccount>(dbContext), IGLAccountRepository
{
    public async Task<GLAccount?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<GLAccount>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
