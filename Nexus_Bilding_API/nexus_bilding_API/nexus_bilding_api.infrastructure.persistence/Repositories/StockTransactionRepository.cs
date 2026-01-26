using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence.Repositories;

public class StockTransactionRepository : GenericRepository<StockTransaction>, IStockTransactionRepository
{
    private readonly NexusBillingContext _context;
    public StockTransactionRepository(NexusBillingContext context) : base(context)
    {
        _context = context;
    }
    
}