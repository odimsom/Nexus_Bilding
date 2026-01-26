using nexus_bilding_api.core.domain.Entities;
using nexus_bilding_api.core.domain.Interfaces;
using nexus_bilding_api.infrastructure.persistence.Context;
using nexus_bilding_api.infrastructure.persistence.Repositories.Base;

namespace nexus_bilding_api.infrastructure.persistence.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    private readonly NexusBillingContext _context;
    public PaymentRepository(NexusBillingContext Context) : base(Context)
    {
        _context = Context;
    }
}