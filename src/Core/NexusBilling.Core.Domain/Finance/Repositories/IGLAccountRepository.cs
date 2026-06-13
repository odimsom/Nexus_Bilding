using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLAccountRepository : IGenericRepository<GLAccount>
{
    Task<GLAccount?> GetByNoAsync(Guid tenantId, string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<GLAccount> Accounts, int Total)> ListAsync(
        Guid tenantId, string? search, bool? blocked, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
