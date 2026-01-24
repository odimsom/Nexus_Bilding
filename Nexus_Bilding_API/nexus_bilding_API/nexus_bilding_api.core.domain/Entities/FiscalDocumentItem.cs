using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.domain.Entities;

public class FiscalDocumentItem : BaseEntity
{
    public Guid FiscalDocumentId { get; set; }
    public FiscalDocument FiscalDocument { get; set; } = null!;

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public double Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    
    public ItbisRate ItbisRate { get; set; }
    public decimal ItbisAmount { get; set; }
    
    public string? ExemptionReason { get; set; }
    
    public decimal? Discount { get; set; }
    public decimal Total { get; set; }
}
