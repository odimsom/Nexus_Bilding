using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Purchasing.Repositories;

public interface IPurchaseHeaderRepository : IGenericRepository<PurchaseHeader>
{
    Task<PurchaseHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<PurchaseHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
