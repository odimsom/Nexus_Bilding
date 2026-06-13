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
        Address2 = string.Empty;
        City = string.Empty;
        Province = string.Empty;
        Country = string.Empty;
        Contact = string.Empty;
        PhoneNo = string.Empty;
        PhoneNo2 = string.Empty;
        Email = string.Empty;
        WebSite = string.Empty;
        Rnc = string.Empty;
        PaymentTermsCode = string.Empty;
        PaymentMethodCode = string.Empty;
        CurrencyCode = string.Empty;
        VendorType = string.Empty;
        TenantId = null!;
    }

    private Vendor(TenantIdentifier tenantId, string no, string name, string address, string city, string contact)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        No = no;
        Name = name;
        Address = address;
        Address2 = string.Empty;
        City = city;
        Province = string.Empty;
        Country = string.Empty;
        Contact = contact;
        PhoneNo = string.Empty;
        PhoneNo2 = string.Empty;
        Email = string.Empty;
        WebSite = string.Empty;
        Rnc = string.Empty;
        PaymentTermsCode = string.Empty;
        PaymentMethodCode = string.Empty;
        CurrencyCode = string.Empty;
        CreditLimit = 0m;
        VendorType = string.Empty;
        Blocked = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string Country { get; private set; }
    public string Contact { get; private set; }
    public string PhoneNo { get; private set; }
    public string PhoneNo2 { get; private set; }
    public string Email { get; private set; }
    public string WebSite { get; private set; }
    public string Rnc { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CreditLimit { get; private set; }
    public string VendorType { get; private set; }
    public bool Blocked { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<Vendor, DomainError> Create(
        TenantIdentifier tenantId,
        string no,
        string name,
        string address = "",
        string address2 = "",
        string city = "",
        string province = "",
        string country = "",
        string contact = "",
        string phoneNo = "",
        string phoneNo2 = "",
        string email = "",
        string webSite = "",
        string rnc = "",
        string paymentTermsCode = "",
        string paymentMethodCode = "",
        string currencyCode = "",
        decimal creditLimit = 0m,
        string vendorType = "")
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.no_required", "El número de proveedor es obligatorio."));

        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<Vendor, DomainError>.Fail(DomainError.Validation("vendor.name_required", "El nombre es obligatorio."));

        var vendor = new Vendor(tenantId, no.Trim(), name.Trim(), address.Trim(), city.Trim(), contact.Trim());
        vendor.Address2 = address2.Trim();
        vendor.Province = province.Trim();
        vendor.Country = country.Trim();
        vendor.PhoneNo = phoneNo.Trim();
        vendor.PhoneNo2 = phoneNo2.Trim();
        vendor.Email = email.Trim();
        vendor.WebSite = webSite.Trim();
        vendor.Rnc = rnc.Trim();
        vendor.PaymentTermsCode = paymentTermsCode.Trim();
        vendor.PaymentMethodCode = paymentMethodCode.Trim();
        vendor.CurrencyCode = currencyCode.Trim();
        vendor.CreditLimit = creditLimit;
        vendor.VendorType = vendorType.Trim();
        return OperationResult<Vendor, DomainError>.Ok(vendor);
    }

    public void Update(
        string name,
        string address = "",
        string address2 = "",
        string city = "",
        string province = "",
        string country = "",
        string contact = "",
        string phoneNo = "",
        string phoneNo2 = "",
        string email = "",
        string webSite = "",
        string rnc = "",
        string paymentTermsCode = "",
        string paymentMethodCode = "",
        string currencyCode = "",
        decimal creditLimit = 0m,
        string vendorType = "")
    {
        Name = name.Trim();
        Address = address.Trim();
        Address2 = address2.Trim();
        City = city.Trim();
        Province = province.Trim();
        Country = country.Trim();
        Contact = contact.Trim();
        PhoneNo = phoneNo.Trim();
        PhoneNo2 = phoneNo2.Trim();
        Email = email.Trim();
        WebSite = webSite.Trim();
        Rnc = rnc.Trim();
        PaymentTermsCode = paymentTermsCode.Trim();
        PaymentMethodCode = paymentMethodCode.Trim();
        CurrencyCode = currencyCode.Trim();
        CreditLimit = creditLimit;
        VendorType = vendorType.Trim();
        UpdatedAt = DateTime.UtcNow;
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
