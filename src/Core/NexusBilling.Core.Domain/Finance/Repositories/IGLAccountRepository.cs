using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Finance.Repositories;

public interface IGLAccountRepository : IGenericRepository<GLAccount>
{
    Task<GLAccount?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
}
