using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class PurchaseLineRepository : GenericRepository<PurchaseLine>, IPurchaseLineRepository
{
    private readonly NexusBillingDbContext _dbContext;

    public PurchaseLineRepository(NexusBillingDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<PurchaseLine>> GetByDocumentNoAsync(short documentType, string documentNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<PurchaseLine>()
            .Where(l => l.DocumentType == documentType && l.DocumentNo == documentNo)
            .ToListAsync(cancellationToken);
    }
}
