using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class ServiceLedgerEntryRepository(NexusBillingDbContext dbContext) : GenericRepository<ServiceLedgerEntry>(dbContext), IServiceLedgerEntryRepository
{
}
