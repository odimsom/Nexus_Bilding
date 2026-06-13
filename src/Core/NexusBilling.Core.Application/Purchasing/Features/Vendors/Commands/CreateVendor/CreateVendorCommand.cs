using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.CreateVendor;

public record CreateVendorCommand(
    Guid TenantId,
    string Name,
    string Address = "",
    string Address2 = "",
    string City = "",
    string Province = "",
    string Country = "",
    string Contact = "",
    string PhoneNo = "",
    string PhoneNo2 = "",
    string Email = "",
    string WebSite = "",
    string Rnc = "",
    string PaymentTermsCode = "",
    string PaymentMethodCode = "",
    string CurrencyCode = "",
    decimal CreditLimit = 0m,
    string VendorType = "") : IRequest<string>;
