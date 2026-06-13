using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendors;

public record GetVendorsQuery(Guid TenantId, int Page, int PageSize) : IRequest<(IReadOnlyList<VendorDto> Items, int TotalCount)>;

public record VendorDto(
    string No,
    string Name,
    string Address,
    string Address2,
    string City,
    string Province,
    string Country,
    string Contact,
    string PhoneNo,
    string PhoneNo2,
    string Email,
    string WebSite,
    string Rnc,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string CurrencyCode,
    decimal CreditLimit,
    string VendorType,
    bool Blocked);
