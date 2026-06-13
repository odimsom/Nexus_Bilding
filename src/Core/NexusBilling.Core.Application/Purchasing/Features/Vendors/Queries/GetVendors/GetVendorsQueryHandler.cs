using MediatR;
using NexusBilling.Core.Application.Common.Interfaces;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendors;

public sealed class GetVendorsQueryHandler(IVendorRepository vendorRepo)
    : IRequestHandler<GetVendorsQuery, (IReadOnlyList<VendorDto> Items, int TotalCount)>
{
    public async Task<(IReadOnlyList<VendorDto> Items, int TotalCount)> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await vendorRepo.ListAsync(
            request.TenantId,
            null, // search
            request.Page,
            request.PageSize,
            cancellationToken);

        var dtos = items.Select(v => new VendorDto(
            v.No,
            v.Name,
            v.Address,
            v.City,
            v.Contact,
            v.Blocked)).ToList();

        return (dtos, total);
    }
}