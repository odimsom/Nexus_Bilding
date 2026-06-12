using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Core.Domain.Administration.Repositories;

public interface INoSeriesLineRepository : IGenericRepository<NoSeriesLine>
{
    Task<NoSeriesLine?> GetActiveLineAsync(Guid tenantId, string seriesCode, CancellationToken ct = default);
    Task<IReadOnlyList<NoSeriesLine>> GetLinesForSeriesAsync(Guid tenantId, string seriesCode, CancellationToken ct = default);
}
