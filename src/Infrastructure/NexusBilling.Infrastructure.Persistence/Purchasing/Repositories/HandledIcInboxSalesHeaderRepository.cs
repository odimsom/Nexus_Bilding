using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Repositories;

public class HandledIcInboxSalesHeaderRepository(NexusBillingDbContext dbContext) : GenericRepository<HandledIcInboxSalesHeader>(dbContext), IHandledIcInboxSalesHeaderRepository
{
}
