using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly NexusBillingContext _context;
    public UserRepository(NexusBillingContext context) : base(context)
    {
        _context = context;
    }
}
