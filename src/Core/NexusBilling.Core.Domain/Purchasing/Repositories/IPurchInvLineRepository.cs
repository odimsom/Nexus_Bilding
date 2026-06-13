using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Core.Domain.Purchasing.Repositories;

public interface IPurchInvLineRepository : IGenericRepository<PurchInvLine>
{
    Task<IEnumerable<PurchInvLine>> GetByDocumentNoAsync(string documentNo, CancellationToken cancellationToken = default);
}
