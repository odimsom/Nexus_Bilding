using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Finance.Repositories;

public class FaSetupRepository(NexusBillingDbContext dbContext) : GenericRepository<FaSetup>(dbContext), IFaSetupRepository
{
}
