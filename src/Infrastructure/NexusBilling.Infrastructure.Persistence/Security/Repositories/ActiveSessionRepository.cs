using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class ActiveSessionRepository(NexusBillingDbContext dbContext) 
    : GenericRepository<ActiveSession>(dbContext), IActiveSessionRepository
{
    public async Task<ActiveSession?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ActiveSession>()
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);
    }

    public async Task<IEnumerable<ActiveSession>> GetByUserSidAsync(Guid userSid, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ActiveSession>()
            .Where(x => x.UserSid == userSid)
            .ToListAsync(cancellationToken);
    }

    public async Task<ActiveSession?> GetBySessionUniqueIdAsync(Guid sessionUniqueId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ActiveSession>()
            .FirstOrDefaultAsync(x => x.SessionUniqueId == sessionUniqueId, cancellationToken);
    }
}
