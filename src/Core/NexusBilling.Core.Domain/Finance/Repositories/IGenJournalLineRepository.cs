using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Finance.Entities;

using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGenJournalLineRepository : IGenericRepository<GenJournalLine>
{
    Task<IReadOnlyList<GenJournalLine>> GetLinesAsync(Guid tenantId, string templateName, string batchName, CancellationToken cancellationToken = default);
}
