using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public sealed class GenJournalLineRepository(NexusBillingDbContext dbContext)
    : GenericRepository<GenJournalLine>(dbContext), IGenJournalLineRepository
{
    private readonly NexusBillingDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<GenJournalLine>> GetLinesAsync(Guid tenantId, string templateName, string batchName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<GenJournalLine>()
            .Where(x => x.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(tenantId) && 
                        x.JournalTemplateName == templateName && 
                        x.JournalBatchName == batchName)
            .OrderBy(x => x.LineNo)
            .ToListAsync(cancellationToken);
    }
}
