using MediatR;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.UpsertVendor;

public record UpsertVendorCommand(
    Guid TenantId,
    string? ExistingNo,
    string? No,
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
    string VendorType = "") : IRequest<UpsertVendorResult>;

public record UpsertVendorResult(bool Created, string No);
