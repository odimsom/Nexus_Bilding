using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserGroupMemberRepository(NexusBillingDbContext dbContext)
    : GenericRepository<UserGroupMember>(dbContext), IUserGroupMemberRepository
{
    public async Task<IReadOnlyList<UserGroupMember>> GetForUserAsync(Guid tenantId, Guid userSecurityId, CancellationToken ct = default)
        => await _dbContext.Set<UserGroupMember>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.UserSecurityId == userSecurityId)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<UserGroupMember>> GetForGroupAsync(Guid tenantId, string groupCode, CancellationToken ct = default)
        => await _dbContext.Set<UserGroupMember>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId) && x.UserGroupCode == groupCode)
            .ToListAsync(ct);

    public async Task DeleteForUserAsync(Guid tenantId, Guid userSecurityId, CancellationToken ct = default)
    {
        var members = await GetForUserAsync(tenantId, userSecurityId, ct);
        foreach (var m in members)
            _dbContext.Set<UserGroupMember>().Remove(m);
    }
}
