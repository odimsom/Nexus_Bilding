using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class CustomerRepository(NexusBillingDbContext dbContext)
    : GenericRepository<Customer>(dbContext), ICustomerRepository
{
    public async Task<Customer?> GetByNoAsync(string no, CancellationToken ct = default)
        => await _dbContext.Customers.FirstOrDefaultAsync(x => x.No == no, ct);

    public async Task<Customer?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        return await _dbContext.Customers
            .FirstOrDefaultAsync(x => x.TenantId == tid && x.No == no, ct);
    }

    public async Task<(IReadOnlyList<Customer> Items, int Total)> ListAsync(
        Guid tenantId, string? search, bool? blocked, int page, int pageSize,
        CancellationToken ct = default)
    {
        var tid = TenantIdentifier.Create(tenantId);
        var query = _dbContext.Customers.Where(c => c.TenantId == tid);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.ToLower();
            query = query.Where(c =>
                c.No.ToLower().Contains(q) ||
                c.Name.ToLower().Contains(q) ||
                c.City.ToLower().Contains(q) ||
                c.Contact.ToLower().Contains(q));
        }

        if (blocked.HasValue)
            query = query.Where(c => c.Blocked == blocked.Value);

        var total = await query.CountAsync(ct);
        var ps = Math.Clamp(pageSize, 1, 200);
        var pg = Math.Max(1, page);

        var items = (IReadOnlyList<Customer>)await query
            .OrderBy(c => c.Name)
            .Skip((pg - 1) * ps)
            .Take(ps)
            .ToListAsync(ct);

        return (items, total);
    }
}
