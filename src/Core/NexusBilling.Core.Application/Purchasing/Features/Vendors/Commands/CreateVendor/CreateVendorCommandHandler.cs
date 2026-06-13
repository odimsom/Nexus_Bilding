using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.CreateVendor;

public sealed class CreateVendorCommandHandler(
    IVendorRepository vendorRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<CreateVendorCommand, string>
{
    public async Task<string> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);

        string no = await noSeriesService.GetNextNoAsync(request.TenantId, "VEND", cancellationToken);

        var result = Vendor.Create(
            tenantId,
            no,
            request.Name,
            request.Address,
            request.Address2,
            request.City,
            request.Province,
            request.Country,
            request.Contact,
            request.PhoneNo,
            request.PhoneNo2,
            request.Email,
            request.WebSite,
            request.Rnc,
            request.PaymentTermsCode,
            request.PaymentMethodCode,
            request.CurrencyCode,
            request.CreditLimit,
            request.VendorType);

        if (!result.IsSuccess)
            throw new InvalidOperationException($"Vendor creation failed: {result.GetError().Message}");

        var vendor = result.GetValue()!;
        await vendorRepo.AddAsync(vendor, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return no;
    }
}
