using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class AnalysisColumnRepository(NexusBillingDbContext dbContext) : GenericRepository<AnalysisColumn>(dbContext), IAnalysisColumnRepository
{
}
