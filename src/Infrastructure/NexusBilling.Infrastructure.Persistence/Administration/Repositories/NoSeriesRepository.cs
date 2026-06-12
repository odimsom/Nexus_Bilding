using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class NoSeriesRepository(NexusBillingDbContext dbContext) : GenericRepository<NoSeries>(dbContext), INoSeriesRepository
{
    public async Task<NoSeries?> GetByCodeForTenantAsync(Guid tenantId, string code, CancellationToken ct = default)
        => await _dbContext.Set<NoSeries>()
            .FirstOrDefaultAsync(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.Code == code, ct);

    public async Task<IReadOnlyList<NoSeries>> GetAllForTenantAsync(Guid tenantId, CancellationToken ct = default)
        => await _dbContext.Set<NoSeries>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId))
            .OrderBy(x => x.Code)
            .ToListAsync(ct);
}
