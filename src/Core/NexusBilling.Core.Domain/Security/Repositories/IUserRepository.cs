using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllForTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<User?> GetByIdForTenantAsync(Guid tenantId, Guid userId, CancellationToken cancellationToken = default);
}
