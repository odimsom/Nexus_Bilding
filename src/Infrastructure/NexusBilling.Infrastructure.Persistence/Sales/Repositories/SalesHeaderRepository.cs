using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class SalesHeaderRepository(NexusBillingDbContext dbContext)
    : GenericRepository<SalesHeader>(dbContext), ISalesHeaderRepository
{
    public async Task<SalesHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
        => await _dbContext.Set<SalesHeader>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);

    public async Task<SalesHeader?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken cancellationToken = default)
        => await _dbContext.Set<SalesHeader>()
            .FirstOrDefaultAsync(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.No == no, cancellationToken);

    public async Task<(IReadOnlyList<SalesHeader> Items, int TotalCount)> GetPagedForTenantAsync(
        Guid tenantId, string? documentType, string? status, string? search,
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        var query = _dbContext.Set<SalesHeader>().Where(x => x.TenantId == tid);

        if (!string.IsNullOrEmpty(documentType))
            query = query.Where(x => x.DocumentType == documentType);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(x => x.Status == status);

        if (!string.IsNullOrEmpty(search))
        {
            var s = search.ToLower();
            query = query.Where(x =>
                x.No.ToLower().Contains(s) ||
                x.SellToCustomerName.ToLower().Contains(s) ||
                x.SellToCustomerNo.ToLower().Contains(s) ||
                x.ExternalDocumentNo.ToLower().Contains(s));
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
