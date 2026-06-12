using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class NoSeriesLineRepository(NexusBillingDbContext dbContext) : GenericRepository<NoSeriesLine>(dbContext), INoSeriesLineRepository
{
    public async Task<NoSeriesLine?> GetActiveLineAsync(Guid tenantId, string seriesCode, CancellationToken ct = default)
        => await _dbContext.Set<NoSeriesLine>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId) &&
                        x.SeriesCode == seriesCode &&
                        x.Open)
            .OrderBy(x => x.LineNo)
            .FirstOrDefaultAsync(ct);

    public async Task<IReadOnlyList<NoSeriesLine>> GetLinesForSeriesAsync(Guid tenantId, string seriesCode, CancellationToken ct = default)
        => await _dbContext.Set<NoSeriesLine>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.SeriesCode == seriesCode)
            .OrderBy(x => x.LineNo)
            .ToListAsync(ct);
}
