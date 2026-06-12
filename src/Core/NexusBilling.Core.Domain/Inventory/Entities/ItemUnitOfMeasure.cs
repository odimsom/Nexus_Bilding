using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemUnitOfMeasure : Entity
{
    private readonly List<object> _domainEvents = [];

    private ItemUnitOfMeasure()
    {
        ItemNo = string.Empty;
        Code = string.Empty;
        TenantId = null!;
    }

    private ItemUnitOfMeasure(TenantIdentifier tenantId, string itemNo, string code, decimal qtyPerUnitOfMeasure)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        ItemNo = itemNo;
        Code = code;
        QtyPerUnitOfMeasure = qtyPerUnitOfMeasure;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string Code { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<ItemUnitOfMeasure, DomainError> Create(
        TenantIdentifier tenantId,
        string itemNo,
        string code,
        decimal qtyPerUnitOfMeasure)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<ItemUnitOfMeasure, DomainError>.Fail(DomainError.Validation("item_uom.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(itemNo))
            return OperationResult<ItemUnitOfMeasure, DomainError>.Fail(DomainError.Validation("item_uom.item_no_required", "El número de artículo es obligatorio."));

        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<ItemUnitOfMeasure, DomainError>.Fail(DomainError.Validation("item_uom.code_required", "El código es obligatorio."));

        if (qtyPerUnitOfMeasure <= 0)
            return OperationResult<ItemUnitOfMeasure, DomainError>.Fail(DomainError.Validation("item_uom.qty_must_be_positive", "La cantidad debe ser mayor que cero."));

        return OperationResult<ItemUnitOfMeasure, DomainError>.Ok(new ItemUnitOfMeasure(tenantId, itemNo.Trim(), code.Trim(), qtyPerUnitOfMeasure));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
