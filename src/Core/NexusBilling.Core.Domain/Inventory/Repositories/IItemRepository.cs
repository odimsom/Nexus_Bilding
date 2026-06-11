using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Inventory.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item?> GetByNoAsync(string no, CancellationToken cancellationToken = default);
}
