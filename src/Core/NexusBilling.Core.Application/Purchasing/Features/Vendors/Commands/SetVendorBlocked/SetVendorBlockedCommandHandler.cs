using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.SetVendorBlocked;

public sealed class SetVendorBlockedCommandHandler(
    IVendorRepository vendorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<SetVendorBlockedCommand, bool>
{
    public async Task<bool> Handle(SetVendorBlockedCommand request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);
        var vendors = await vendorRepository.FindAsync(v => v.TenantId == tenantId && v.No == request.No);
        var vendor = vendors.FirstOrDefault();

        if (vendor == null)
            return false;

        if (request.Blocked)
            vendor.Block();
        else
            vendor.Unblock();

        await vendorRepository.UpdateAsync(vendor);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
