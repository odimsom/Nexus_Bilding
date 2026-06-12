using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Core.Domain.Administration.Repositories;

public interface INoSeriesRepository : IGenericRepository<NoSeries>
{
    Task<NoSeries?> GetByCodeForTenantAsync(Guid tenantId, string code, CancellationToken ct = default);
    Task<IReadOnlyList<NoSeries>> GetAllForTenantAsync(Guid tenantId, CancellationToken ct = default);
}
