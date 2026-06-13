using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendors;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendorByNo;

public sealed class GetVendorByNoQueryHandler(
    IVendorRepository vendorRepository) : IRequestHandler<GetVendorByNoQuery, VendorDto?>
{
    public async Task<VendorDto?> Handle(GetVendorByNoQuery request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);
        var vendors = await vendorRepository.FindAsync(v => v.TenantId == tenantId && v.No == request.No);
        var vendor = vendors.FirstOrDefault();

        if (vendor == null)
            return null;

        return new VendorDto(
            vendor.No,
            vendor.Name,
            vendor.Address,
            vendor.Address2,
            vendor.City,
            vendor.Province,
            vendor.Country,
            vendor.Contact,
            vendor.PhoneNo,
            vendor.PhoneNo2,
            vendor.Email,
            vendor.WebSite,
            vendor.Rnc,
            vendor.PaymentTermsCode,
            vendor.PaymentMethodCode,
            vendor.CurrencyCode,
            vendor.CreditLimit,
            vendor.VendorType,
            vendor.Blocked);
    }
}
