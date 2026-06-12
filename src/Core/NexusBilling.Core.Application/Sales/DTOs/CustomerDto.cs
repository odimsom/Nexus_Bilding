namespace NexusBilling.Core.Application.Sales.DTOs;

public record CustomerDto(
    string No, string Name, string Address, string City, string Contact, bool Blocked,
    string PhoneNo, string Email, decimal CreditLimit, decimal Balance, decimal BalanceDue,
    string VatRegistrationNo, string PaymentTermsCode, string PaymentMethodCode,
    string SalespersonCode, string CurrencyCode, string CustomerPostingGroup, string CountryRegionCode);

public record CustomerListDto(
    string No, string Name, string City, string Contact, bool Blocked,
    string SalespersonCode, string PaymentTermsCode, decimal Balance, decimal BalanceDue);
