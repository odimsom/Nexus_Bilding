using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLEntryRepository : IGenericRepository<GLEntry>
{
    Task<(IReadOnlyList<GLEntry> Entries, int Total)> ListAsync(
        Guid tenantId, string? glAccountNo, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
