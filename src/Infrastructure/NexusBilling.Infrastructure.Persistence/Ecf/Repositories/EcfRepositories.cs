using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Ecf.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Ecf.Repositories;

public class EcfCompanyConfigRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<EcfCompanyConfig>(dbContext), IEcfCompanyConfigRepository
{
}

public class EcfNcfSequenceRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<EcfNcfSequence>(dbContext), IEcfNcfSequenceRepository
{
}

public class EcfDocumentRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<EcfDocument>(dbContext), IEcfDocumentRepository
{
}
