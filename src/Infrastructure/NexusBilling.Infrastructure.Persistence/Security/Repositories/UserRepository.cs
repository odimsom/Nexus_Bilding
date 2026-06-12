using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserRepository(NexusBillingDbContext dbContext)
    : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
            .AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<IReadOnlyList<User>> GetAllForTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId))
            .OrderBy(x => x.Username)
            .ToListAsync(cancellationToken);

    public async Task<User?> GetByIdForTenantAsync(Guid tenantId, Guid userId, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.Id == userId, cancellationToken);
}
