using System;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class VendorRepository(NexusBillingDbContext dbContext)
    : GenericRepository<Vendor>(dbContext), IVendorRepository
{
    public async Task<Vendor?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Vendor>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }

    public async Task<(IReadOnlyList<Vendor> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<Vendor>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId));

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.ToLower();
            query = query.Where(x => x.No.ToLower().Contains(s) || x.Name.ToLower().Contains(s));
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(x => x.No)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, total);
    }
}
