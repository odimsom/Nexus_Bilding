using MediatR;
using NexusBilling.Core.Application.Common.Interfaces;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrders;

public sealed class GetPurchaseOrdersQueryHandler(IPurchaseHeaderRepository orderRepo)
    : IRequestHandler<GetPurchaseOrdersQuery, (IReadOnlyList<PurchaseOrderDto> Items, int TotalCount)>
{
    public async Task<(IReadOnlyList<PurchaseOrderDto> Items, int TotalCount)> Handle(GetPurchaseOrdersQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await orderRepo.ListAsync(
            request.TenantId,
            null, // search
            request.Page,
            request.PageSize,
            cancellationToken);

        var dtos = items.Select(o => new PurchaseOrderDto(
            o.No,
            o.BuyFromVendorNo,
            o.PayToName,
            o.PostingDate,
            o.Status,
            o.Amount,
            o.AmountIncludingVat,
            o.CurrencyCode)).ToList();

        return (dtos, total);
    }
}