using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Core.Domain.Sales.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<Customer?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Customer> Items, int Total)> ListAsync(
        Guid tenantId, string? search, bool? blocked, int page, int pageSize,
        CancellationToken cancellationToken = default);
}
