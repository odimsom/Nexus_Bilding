using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence.Repositories;

public class FiscalDocumentRepository : GenericRepository<FiscalDocument>, IFiscalDocumentRepository
{
    private readonly NexusBillingContext _context;
    public FiscalDocumentRepository(NexusBillingContext Context) : base(Context)
    {
        _context = Context;
    }
    
}