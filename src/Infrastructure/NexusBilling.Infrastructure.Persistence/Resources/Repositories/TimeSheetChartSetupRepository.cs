using NexusBilling.Core.Domain.Resources.Entities;
using NexusBilling.Core.Domain.Resources.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Resources.Repositories;

public class TimeSheetChartSetupRepository(NexusBillingDbContext dbContext) : GenericRepository<TimeSheetChartSetup>(dbContext), ITimeSheetChartSetupRepository
{
}
