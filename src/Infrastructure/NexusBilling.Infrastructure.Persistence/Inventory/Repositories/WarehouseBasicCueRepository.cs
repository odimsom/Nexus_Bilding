using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Repositories;

public class WarehouseBasicCueRepository(NexusBillingDbContext dbContext) : GenericRepository<WarehouseBasicCue>(dbContext), IWarehouseBasicCueRepository
{
}
