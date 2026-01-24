using nexus_bilding_api.core.domain.Common;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.core.domain.Entities;

public class StockTransaction : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public StockTransactionType Type { get; set; }
    
    public double Quantity { get; set; }
    public double BalanceAfter { get; set; }
    
    public string? Reference { get; set; }
}
