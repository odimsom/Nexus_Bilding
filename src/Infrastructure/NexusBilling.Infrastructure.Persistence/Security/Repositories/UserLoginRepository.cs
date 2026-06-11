using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserLoginRepository(NexusBillingDbContext dbContext) : GenericRepository<UserLogin>(dbContext), IUserLoginRepository
{
}
