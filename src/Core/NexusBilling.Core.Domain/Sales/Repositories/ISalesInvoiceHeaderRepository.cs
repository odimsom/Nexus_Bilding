using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ISalesInvoiceHeaderRepository : IGenericRepository<SalesInvoiceHeader>
{
    Task<SalesInvoiceHeader?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<SalesInvoiceHeader> Items, int Total)> ListAsync(
        Guid tenantId, string? search, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
