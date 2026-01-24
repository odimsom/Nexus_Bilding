using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.domain.Entities;

public class Payment : BaseEntity
{
    public Guid FiscalDocumentId { get; set; }
    public FiscalDocument FiscalDocument { get; set; } = null!;
    
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    
    public DateTime PaidAt { get; set; }
}
