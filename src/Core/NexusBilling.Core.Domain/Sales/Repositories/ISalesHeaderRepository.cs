using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ISalesHeaderRepository : IGenericRepository<SalesHeader>
{
    Task<SalesHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<SalesHeader?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<SalesHeader> Items, int TotalCount)> GetPagedForTenantAsync(
        Guid tenantId,
        string? documentType,
        string? status,
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
