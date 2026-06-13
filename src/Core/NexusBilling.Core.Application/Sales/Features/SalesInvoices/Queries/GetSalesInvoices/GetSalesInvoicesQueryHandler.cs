using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoices;

public sealed class GetSalesInvoicesQueryHandler(ISalesInvoiceHeaderRepository repo)
    : IRequestHandler<GetSalesInvoicesQuery, GetSalesInvoicesResult>
{
    public async Task<GetSalesInvoicesResult> Handle(GetSalesInvoicesQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await repo.ListAsync(request.TenantId, request.Search, request.Page, request.PageSize, cancellationToken);

        var dtos = items.Select(i => new SalesInvoiceDto(
            i.Id.ToString(),
            i.No,
            i.SellToCustomerNo,
            i.BillToName,
            i.PostingDate ?? DateTime.MinValue,
            i.AmountIncludingVat)).ToList();

        return new GetSalesInvoicesResult(dtos, total);
    }
}
