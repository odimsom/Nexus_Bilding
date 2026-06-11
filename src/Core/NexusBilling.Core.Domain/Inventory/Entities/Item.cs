using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class Item : Entity
{
    private readonly List<object> _domainEvents = [];

    // Constructor privado para EF Core y Rehidratación
    private Item()
    {
        No = string.Empty;
        Description = string.Empty;
        BaseUnitOfMeasure = string.Empty;
        TenantId = TenantIdentifier.Create(Guid.Empty);
    }

    private Item(TenantIdentifier tenantId, string no, string description, string baseUnitOfMeasure, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        No = no;
        Description = description;
        BaseUnitOfMeasure = baseUnitOfMeasure;
        UnitPrice = unitPrice;
        UnitCost = 0;
        Blocked = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string BaseUnitOfMeasure { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool Blocked { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<Item, DomainError> Create(
        TenantIdentifier tenantId,
        string no,
        string description,
        string baseUnitOfMeasure,
        decimal unitPrice)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.no_required", "El número de artículo es obligatorio."));

        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<Item, DomainError>.Fail(DomainError.Validation("item.description_required", "La descripción es obligatoria."));

        return OperationResult<Item, DomainError>.Ok(new Item(tenantId, no.Trim(), description.Trim(), baseUnitOfMeasure.Trim(), unitPrice));
    }

    public OperationResult<Unit, DomainError> UpdateCost(decimal newCost)
    {
        if (newCost < 0)
            return OperationResult<Unit, DomainError>.Fail(DomainError.Business("item.cost_cannot_be_negative", "El costo no puede ser negativo."));

        UnitCost = newCost;
        UpdatedAt = DateTime.UtcNow;
        return OperationResult<Unit, DomainError>.Ok(Unit.Value);
    }

    public void Block()
    {
        Blocked = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unblock()
    {
        Blocked = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
