using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Inventory.Repositories;

public interface IItemLedgerEntryRepository : IGenericRepository<ItemLedgerEntry>
{
    Task<ItemLedgerEntry?> GetByEntryNoAsync(int entryNo, CancellationToken cancellationToken = default);
}
