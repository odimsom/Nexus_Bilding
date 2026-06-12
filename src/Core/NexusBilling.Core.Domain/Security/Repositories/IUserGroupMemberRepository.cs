using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IUserGroupMemberRepository : IGenericRepository<UserGroupMember>
{
    Task<IReadOnlyList<UserGroupMember>> GetForUserAsync(Guid tenantId, Guid userSecurityId, CancellationToken ct = default);
    Task<IReadOnlyList<UserGroupMember>> GetForGroupAsync(Guid tenantId, string groupCode, CancellationToken ct = default);
    Task DeleteForUserAsync(Guid tenantId, Guid userSecurityId, CancellationToken ct = default);
}
