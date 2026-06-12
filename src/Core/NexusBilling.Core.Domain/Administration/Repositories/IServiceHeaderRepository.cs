using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Core.Domain.Administration.Repositories;

public interface IServiceHeaderRepository : IGenericRepository<ServiceHeader>
{
    Task<ServiceHeader?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken ct = default);
    Task<(IReadOnlyList<ServiceHeader> Items, int TotalCount)> GetPagedForTenantAsync(
        Guid tenantId, string? docType, string? status, string? search,
        int page, int pageSize, CancellationToken ct = default);
}
