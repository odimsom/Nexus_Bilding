using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Inventory.Repositories;

public interface IItemLedgerEntryRepository : IGenericRepository<ItemLedgerEntry>
{
    Task<ItemLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ItemLedgerEntry>> GetForItemAsync(Guid tenantId, string itemNo, int page, int pageSize, CancellationToken ct = default);
    Task<int> GetTotalForItemAsync(Guid tenantId, string itemNo, CancellationToken ct = default);
    Task<int> GetNextEntryNoAsync(Guid tenantId, CancellationToken ct = default);
}
