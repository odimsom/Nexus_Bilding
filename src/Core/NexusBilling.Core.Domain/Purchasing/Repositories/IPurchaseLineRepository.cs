using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Purchasing.Repositories;

public interface IPurchaseLineRepository : IGenericRepository<PurchaseLine>
{
    Task<IEnumerable<PurchaseLine>> GetByDocumentNoAsync(short documentType, string documentNo, CancellationToken cancellationToken = default);
}
