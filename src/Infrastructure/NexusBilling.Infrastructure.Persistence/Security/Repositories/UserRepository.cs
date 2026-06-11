using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }
}
