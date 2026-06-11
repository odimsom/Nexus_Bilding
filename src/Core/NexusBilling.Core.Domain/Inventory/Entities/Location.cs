using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class Location : Entity
{
    private readonly List<object> _domainEvents = [];

    private Location()
    {
        Code = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        TenantId = TenantIdentifier.Create(Guid.Empty);
    }

    private Location(TenantIdentifier tenantId, string code, string name, string address, string city)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Code = code;
        Name = name;
        Address = address;
        City = city;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<Location, DomainError> Create(
        TenantIdentifier tenantId,
        string code,
        string name,
        string address = "",
        string city = "")
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Location, DomainError>.Fail(DomainError.Validation("location.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<Location, DomainError>.Fail(DomainError.Validation("location.code_required", "El código de almacén es obligatorio."));

        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<Location, DomainError>.Fail(DomainError.Validation("location.name_required", "El nombre es obligatorio."));

        return OperationResult<Location, DomainError>.Ok(new Location(tenantId, code.Trim(), name.Trim(), address.Trim(), city.Trim()));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
