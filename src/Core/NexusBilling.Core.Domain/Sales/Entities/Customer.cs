using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class Customer : Entity
{
    private Customer()
    {
        No = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        City = string.Empty;
        Contact = string.Empty;
        TenantId = null!;
    }

    private Customer(TenantIdentifier tenantId, string no, string name, string address, string city, string contact)
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
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public bool Blocked { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; set; }
    public decimal BalanceDue { get; set; }
    public string VatRegistrationNo { get; set; } = string.Empty;
    public string PaymentTermsCode { get; set; } = string.Empty;
    public string PaymentMethodCode { get; set; } = string.Empty;
    public string SalespersonCode { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string CustomerPostingGroup { get; set; } = string.Empty;
    public string CountryRegionCode { get; set; } = string.Empty;

    public static OperationResult<Customer, DomainError> Create(
        TenantIdentifier tenantId, string no, string name,
        string address = "", string city = "", string contact = "")
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<Customer, DomainError>.Fail(DomainError.Validation("customer.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<Customer, DomainError>.Fail(DomainError.Validation("customer.no_required", "El número de cliente es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<Customer, DomainError>.Fail(DomainError.Validation("customer.name_required", "El nombre es obligatorio."));

        return OperationResult<Customer, DomainError>.Ok(new Customer(tenantId, no.Trim(), name.Trim(), address.Trim(), city.Trim(), contact.Trim()));
    }

    public void Block() { Blocked = true; UpdatedAt = DateTime.UtcNow; }
    public void Unblock() { Blocked = false; UpdatedAt = DateTime.UtcNow; }
}
