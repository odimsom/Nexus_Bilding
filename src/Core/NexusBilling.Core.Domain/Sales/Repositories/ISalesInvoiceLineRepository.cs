using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ISalesInvoiceLineRepository : IGenericRepository<SalesInvoiceLine>
{
    Task<IEnumerable<SalesInvoiceLine>> GetByDocumentNoAsync(string documentNo, CancellationToken cancellationToken = default);
}
