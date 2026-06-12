using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;
using NexusBilling.Infrastructure.Persistence.Context;
using NexusBilling.Infrastructure.Persistence.Repositories.Base;

namespace NexusBilling.Infrastructure.Persistence.Security.Repositories;

public class UserGroupRepository(NexusBillingDbContext dbContext) : GenericRepository<UserGroup>(dbContext), IUserGroupRepository
{
    public async Task<IReadOnlyList<UserGroup>> GetAllForTenantAsync(Guid tenantId, CancellationToken ct = default)
        => await _dbContext.Set<UserGroup>()
            .Where(x => x.TenantId == TenantIdentifier.Create(tenantId))
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
}
