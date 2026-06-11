using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ISalesHeaderRepository : IGenericRepository<SalesHeader>
{
    Task<SalesHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
}
