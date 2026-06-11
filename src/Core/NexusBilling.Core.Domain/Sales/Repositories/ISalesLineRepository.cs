using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ISalesLineRepository : IGenericRepository<SalesLine>
{
    Task<IEnumerable<SalesLine>> GetByDocumentNoAsync(short documentType, string documentNo, CancellationToken cancellationToken = default);
}
