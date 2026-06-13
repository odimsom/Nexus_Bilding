using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class PurchInvHeaderRepository(NexusBillingDbContext dbContext) : GenericRepository<PurchInvHeader>(dbContext), IPurchInvHeaderRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<PurchInvHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PurchInvHeaders
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }

    public async Task<(IReadOnlyList<PurchInvHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.PurchInvHeaders
            .Where(x => x.TenantId.Value == tenantId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.No.Contains(search) ||
                x.BuyFromVendorNo.Contains(search) ||
                x.PayToName.Contains(search));
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
