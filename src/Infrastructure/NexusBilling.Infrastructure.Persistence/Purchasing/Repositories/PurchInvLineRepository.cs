using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class PurchInvLineRepository(NexusBillingDbContext dbContext) : GenericRepository<PurchInvLine>(dbContext), IPurchInvLineRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<IEnumerable<PurchInvLine>> GetByDocumentNoAsync(string documentNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PurchInvLines
            .Where(x => x.DocumentNo == documentNo)
            .OrderBy(x => x.LineNo)
            .ToListAsync(cancellationToken);
    }
}
