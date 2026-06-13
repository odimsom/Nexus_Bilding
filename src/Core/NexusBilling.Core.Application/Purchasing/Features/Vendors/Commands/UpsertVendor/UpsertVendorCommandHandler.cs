using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.UpsertVendor;

public sealed class UpsertVendorCommandHandler(
    IVendorRepository vendorRepository,
    NoSeriesService noSeriesService,
    IUnitOfWork unitOfWork) : IRequestHandler<UpsertVendorCommand, UpsertVendorResult>
{
    public async Task<UpsertVendorResult> Handle(UpsertVendorCommand request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);
        Vendor? vendor = null;
        var created = false;

        if (!string.IsNullOrEmpty(request.ExistingNo))
        {
            var vendors = await vendorRepository.FindAsync(v => v.TenantId == tenantId && v.No == request.ExistingNo);
            vendor = vendors.FirstOrDefault();
        }

        if (vendor == null)
        {
            var vendorNo = request.No;
            if (string.IsNullOrWhiteSpace(vendorNo))
                vendorNo = await noSeriesService.GetNextNoAsync(request.TenantId, "VEND", cancellationToken);

            var result = Vendor.Create(tenantId, vendorNo, request.Name,
                request.Address, request.Address2, request.City, request.Province, request.Country,
                request.Contact, request.PhoneNo, request.PhoneNo2, request.Email, request.WebSite,
                request.Rnc, request.PaymentTermsCode, request.PaymentMethodCode,
                request.CurrencyCode, request.CreditLimit, request.VendorType);

            if (!result.IsSuccess)
                throw new InvalidOperationException(result.GetError()?.Message ?? "Error al crear proveedor");

            vendor = result.GetValue()!;
            await vendorRepository.AddAsync(vendor);
            created = true;
        }
        else
        {
            vendor.Update(
                request.Name, request.Address, request.Address2, request.City,
                request.Province, request.Country, request.Contact,
                request.PhoneNo, request.PhoneNo2, request.Email, request.WebSite,
                request.Rnc, request.PaymentTermsCode, request.PaymentMethodCode,
                request.CurrencyCode, request.CreditLimit, request.VendorType);
            await vendorRepository.UpdateAsync(vendor);
        }

        await unitOfWork.SaveChangesAsync();
        return new UpsertVendorResult(created, vendor.No);
    }
}
