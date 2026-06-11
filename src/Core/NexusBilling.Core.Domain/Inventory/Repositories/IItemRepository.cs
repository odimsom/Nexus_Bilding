using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Core.Domain.Inventory.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(Guid id);
    Task<IEnumerable<Item>> GetAllAsync();
    Task AddAsync(Item item);
    void Update(Item item);
    void Delete(Item item);
}
