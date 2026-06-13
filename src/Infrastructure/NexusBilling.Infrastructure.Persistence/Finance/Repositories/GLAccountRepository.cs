using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class GLAccountRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<GLAccount>(dbContext), IGLAccountRepository
{
    public async Task<GLAccount?> GetByNoAsync(Guid tenantId, string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<GLAccount>()
            .FirstOrDefaultAsync(x => x.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(tenantId) && x.No == no, cancellationToken);
    }

    public async Task<(IReadOnlyList<GLAccount> Accounts, int Total)> ListAsync(Guid tenantId, string? search, bool? blocked, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<GLAccount>().Where(x => x.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(tenantId));

        if (blocked.HasValue)
            query = query.Where(x => x.Blocked == blocked.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(x => x.No.ToLower().Contains(search) || x.Name.ToLower().Contains(search));
        }

        var total = await query.CountAsync(cancellationToken);
        var accounts = await query
            .OrderBy(x => x.No)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (accounts, total);
    }
}
