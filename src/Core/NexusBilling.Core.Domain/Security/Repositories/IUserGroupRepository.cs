using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IUserGroupRepository : IGenericRepository<UserGroup>
{
    Task<IReadOnlyList<UserGroup>> GetAllForTenantAsync(Guid tenantId, CancellationToken ct = default);
}
