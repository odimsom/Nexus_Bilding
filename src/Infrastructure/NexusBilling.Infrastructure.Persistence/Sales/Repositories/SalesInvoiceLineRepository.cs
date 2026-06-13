using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class SalesInvoiceLineRepository(NexusBillingDbContext dbContext) : GenericRepository<SalesInvoiceLine>(dbContext), ISalesInvoiceLineRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<IEnumerable<SalesInvoiceLine>> GetByDocumentNoAsync(string documentNo, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesInvoiceLines
            .Where(x => x.DocumentNo == documentNo)
            .OrderBy(x => x.LineNo)
            .ToListAsync(cancellationToken);
    }
}
