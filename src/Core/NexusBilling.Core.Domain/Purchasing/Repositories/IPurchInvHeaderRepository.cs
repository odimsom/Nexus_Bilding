using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Core.Domain.Purchasing.Repositories;

public interface IPurchInvHeaderRepository : IGenericRepository<PurchInvHeader>
{
    Task<PurchInvHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<PurchInvHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
