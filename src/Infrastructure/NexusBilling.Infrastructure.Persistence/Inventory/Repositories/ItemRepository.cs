using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Repositories;

public class ItemRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<Item>(dbContext), IItemRepository
{
    public async Task<Item?> GetByNoAsync(string no, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Item>()
            .FirstOrDefaultAsync(x => x.No == no, cancellationToken);
    }
}
