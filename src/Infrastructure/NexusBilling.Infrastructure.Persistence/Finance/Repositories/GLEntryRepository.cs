using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public sealed class GLEntryRepository(NexusBillingDbContext dbContext)
    : GenericRepository<GLEntry>(dbContext), IGLEntryRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<(IReadOnlyList<GLEntry> Entries, int Total)> ListAsync(Guid tenantId, string? glAccountNo, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<GLEntry>().Where(x => x.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(tenantId));

        if (!string.IsNullOrWhiteSpace(glAccountNo))
            query = query.Where(x => x.GLAccountNo == glAccountNo);

        var total = await query.CountAsync(cancellationToken);
        var entries = await query
            .OrderByDescending(x => x.PostingDate)
            .ThenByDescending(x => x.EntryNo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (entries, total);
    }
}
