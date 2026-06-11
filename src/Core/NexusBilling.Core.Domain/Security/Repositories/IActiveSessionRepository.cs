using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IActiveSessionRepository : IGenericRepository<ActiveSession>
{
    Task<ActiveSession?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<ActiveSession>> GetByUserSidAsync(Guid userSid, CancellationToken cancellationToken = default);
    Task<ActiveSession?> GetBySessionUniqueIdAsync(Guid sessionUniqueId, CancellationToken cancellationToken = default);
}
