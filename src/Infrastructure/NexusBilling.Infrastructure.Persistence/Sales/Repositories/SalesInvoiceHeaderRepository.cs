using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class SalesInvoiceHeaderRepository(NexusBillingDbContext dbContext) : GenericRepository<SalesInvoiceHeader>(dbContext), ISalesInvoiceHeaderRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<SalesInvoiceHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SalesInvoiceHeaders
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }

    public async Task<(IReadOnlyList<SalesInvoiceHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.SalesInvoiceHeaders
            .Where(x => x.TenantId.Value == tenantId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.No.Contains(search) ||
                x.SellToCustomerNo.Contains(search) ||
                x.BillToName.Contains(search));
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(x => x.PostingDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, total);
    }
}
