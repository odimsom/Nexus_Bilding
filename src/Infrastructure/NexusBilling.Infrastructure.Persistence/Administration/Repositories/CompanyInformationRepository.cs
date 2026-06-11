using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Administration.Repositories;

public class CompanyInformationRepository(NexusBillingDbContext dbContext) : GenericRepository<CompanyInformation>(dbContext), ICompanyInformationRepository
{
}
