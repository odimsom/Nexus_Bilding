using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class SalesLineRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<SalesLine>(dbContext), ISalesLineRepository
{
    public async Task<IEnumerable<SalesLine>> GetByDocumentNoAsync(short documentType, string documentNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<SalesLine>()
            .Where(x => x.DocumentType == documentType && x.DocumentNo == documentNo)
            .OrderBy(x => x.LineNo)
            .ToListAsync(cancellationToken);
    }
}
