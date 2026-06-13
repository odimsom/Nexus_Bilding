using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Customers.Commands.UpsertCustomer;

public record UpsertCustomerCommand(
    Guid TenantId,
    string? ExistingNo,
    string? No,
    string Name,
    string Address,
    string City,
    string Contact,
    string PhoneNo,
    string Email,
    decimal CreditLimit,
    string VatRegistrationNo,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string CurrencyCode,
    string CustomerPostingGroup,
    string CountryRegionCode
) : IRequest<UpsertCustomerResult>;

public record UpsertCustomerResult(bool Created, string No);
