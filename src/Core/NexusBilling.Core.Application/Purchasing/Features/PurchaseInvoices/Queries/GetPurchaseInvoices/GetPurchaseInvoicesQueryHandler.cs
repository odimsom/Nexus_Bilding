using MediatR;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoices;

public sealed class GetPurchaseInvoicesQueryHandler(IPurchInvHeaderRepository repo)
    : IRequestHandler<GetPurchaseInvoicesQuery, GetPurchaseInvoicesResult>
{
    public async Task<GetPurchaseInvoicesResult> Handle(GetPurchaseInvoicesQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await repo.ListAsync(request.TenantId, request.Search, request.Page, request.PageSize, cancellationToken);

        var dtos = items.Select(i => new PurchaseInvoiceDto(
            i.Id.ToString(),
            i.No,
            i.BuyFromVendorNo,
            i.PayToName,
            i.PostingDate ?? DateTime.MinValue,
            0m)).ToList();

        return new GetPurchaseInvoicesResult(dtos, total);
    }
}
