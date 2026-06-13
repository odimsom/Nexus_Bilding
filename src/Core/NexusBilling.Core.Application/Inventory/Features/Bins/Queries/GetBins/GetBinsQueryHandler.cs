using MediatR;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Features.Bins.Queries.GetBins;

public sealed class GetBinsQueryHandler(IBinRepository repo)
    : IRequestHandler<GetBinsQuery, List<BinDto>>
{
    public async Task<List<BinDto>> Handle(GetBinsQuery request, CancellationToken cancellationToken)
    {
        var bins = await repo.FindAsync(b => b.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(request.TenantId) && b.LocationCode == request.LocationCode, null, cancellationToken);
        return bins.Select(b => new BinDto(b.LocationCode, b.Code, b.Description, b.ZoneCode)).ToList();
    }
}
