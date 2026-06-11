using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Sales.Repositories;

public class StandardSalesLineRepository(NexusBillingDbContext dbContext) : GenericRepository<StandardSalesLine>(dbContext), IStandardSalesLineRepository
{
}
