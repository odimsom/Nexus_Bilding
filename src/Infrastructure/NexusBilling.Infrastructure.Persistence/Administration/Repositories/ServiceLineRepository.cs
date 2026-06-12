using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class ServiceLineRepository(NexusBillingDbContext dbContext) : GenericRepository<ServiceLine>(dbContext), IServiceLineRepository
{
    public async Task<IReadOnlyList<ServiceLine>> GetByDocumentNoAsync(short docType, string documentNo, CancellationToken ct = default)
        => await dbContext.ServiceLines
            .Where(l => l.DocumentType == docType && l.DocumentNo == documentNo)
            .OrderBy(l => l.LineNo)
            .ToListAsync(ct);
}
