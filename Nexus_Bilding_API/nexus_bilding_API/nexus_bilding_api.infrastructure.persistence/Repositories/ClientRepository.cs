using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    private readonly NexusBillingContext _context;
    public ClientRepository(NexusBillingContext Context) : base(Context)
    {
        _context = Context;
    }
    
}