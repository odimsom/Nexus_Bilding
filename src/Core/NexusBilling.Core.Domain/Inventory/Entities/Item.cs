using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class Item : Entity
{
    private Item()
    {
        No = string.Empty;
        Description = string.Empty;
        BaseUnitOfMeasure = string.Empty;
        TenantId = null!;
    }

    private Item(TenantIdentifier tenantId, string no, string description, string baseUnitOfMeasure, decimal unitPrice, decimal unitCost, string type)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        No = no;
        Description = description;
        BaseUnitOfMeasure = baseUnitOfMeasure;
        UnitPrice = unitPrice;
        UnitCost = unitCost;
        Type = type;
        Blocked = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; set; } = string.Empty;
    public string Description2 { get; set; } = string.Empty;
    public string BaseUnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal UnitCost { get; set; }
    public bool Blocked { get; set; }
    public string Type { get; set; } = "Inventory";
    public string ItemCategoryCode { get; set; } = string.Empty;
    public string InventoryPostingGroup { get; set; } = string.Empty;
    public string GenProdPostingGroup { get; set; } = string.Empty;
    public string VatProdPostingGroup { get; set; } = string.Empty;
    public string VendorNo { get; set; } = string.Empty;
    public string VendorItemNo { get; set; } = string.Empty;
    public decimal StandardCost { get; set; }
    public decimal LastDirectCost { get; set; }

    public static OperationResult<Item, DomainError> Create(
        TenantIdentifier tenantId, string no, string description,
        string baseUnitOfMeasure, decimal unitPrice, decimal unitCost = 0, string type = "Inventory")
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.no_required", "El número de artículo es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.description_required", "La descripción es obligatoria."));

        return OperationResult<Item, DomainError>.Ok(new Item(tenantId, no.Trim(), description.Trim(), baseUnitOfMeasure.Trim(), unitPrice, unitCost, type));
    }

    public void Block() { Blocked = true; UpdatedAt = DateTime.UtcNow; }
    public void Unblock() { Blocked = false; UpdatedAt = DateTime.UtcNow; }
}
