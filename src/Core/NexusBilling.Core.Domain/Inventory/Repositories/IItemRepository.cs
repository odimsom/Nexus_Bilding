using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Inventory.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
    Task<Item?> GetByNoForTenantAsync(Guid tenantId, string no, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Item> Items, int Total)> ListAsync(
        Guid tenantId, string? search, bool? blocked, int page, int pageSize,
        CancellationToken cancellationToken = default);
    Task<Dictionary<string, decimal>> GetInventoryByItemNosAsync(
        Guid tenantId, IReadOnlyList<string> itemNos, CancellationToken cancellationToken = default);
    Task<decimal> GetInventoryForItemAsync(
        Guid tenantId, string itemNo, CancellationToken cancellationToken = default);
}
