using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Domain.Inventory.Entities;

/// <summary>
/// Item Aggregate Root.
/// </summary>
public class Item : Entity
{
    public string No { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BaseUnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal UnitCost { get; set; }
    public bool Blocked { get; set; }
    public TenantId TenantId { get; set; } = null!;
}
