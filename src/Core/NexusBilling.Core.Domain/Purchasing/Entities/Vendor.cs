using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class Vendor : Entity
{
    private readonly List<object> _domainEvents = [];

    private Vendor()
    {
        No = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        Contact = string.Empty;
        TenantId = TenantIdentifier.Create(Guid.Empty);
    }

    private Vendor(TenantIdentifier tenantId, string no, string name, string address, string city, string contact)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        No = no;
        Name = name;
        Address = address;
        City = city;
        Contact = contact;
        Blocked = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string Contact { get; private set; }
    public bool Blocked { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<Vendor, DomainError> Create(
        TenantIdentifier tenantId,
        string no,
        string name,
        string address = "",
        string city = "",
        string contact = "")
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.no_required", "El número de proveedor es obligatorio."));

        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.name_required", "El nombre es obligatorio."));

        return OperationResult<Vendor, DomainError>.Ok(new Vendor(tenantId, no.Trim(), name.Trim(), address.Trim(), city.Trim(), contact.Trim()));
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
