using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserSetupRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<UserSetup>(dbContext), IUserSetupRepository
{
    public async Task<UserSetup?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserSetup>()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
