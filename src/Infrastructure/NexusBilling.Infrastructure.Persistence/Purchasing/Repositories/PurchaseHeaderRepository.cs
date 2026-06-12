using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class PurchaseHeaderRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<PurchaseHeader>(dbContext), IPurchaseHeaderRepository
{
    public async Task<PurchaseHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<PurchaseHeader>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }

    public async Task<(IReadOnlyList<PurchaseHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<PurchaseHeader>()
            .Where(x => x.TenantId.Value == tenantId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.ToLower();
            query = query.Where(x => x.No.ToLower().Contains(s) || x.BuyFromVendorNo.ToLower().Contains(s) || x.PayToName.ToLower().Contains(s));
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(x => x.PostingDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, total);
    }
}
