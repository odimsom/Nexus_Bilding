using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Domain.Security.Repositories;

public interface IUserSetupRepository : IGenericRepository<UserSetup>
{
    Task<UserSetup?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
