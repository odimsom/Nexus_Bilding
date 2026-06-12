using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Queries;

public sealed class GetSalesOrdersQueryHandler(ISalesHeaderRepository repo)
    : IRequestHandler<GetSalesOrdersQuery, GetSalesOrdersResult>
{
    public async Task<GetSalesOrdersResult> Handle(GetSalesOrdersQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedForTenantAsync(
            q.TenantId, q.DocumentType, q.Status, q.Search, q.Page, q.PageSize, ct);

        var dtos = items.Select(h => new SalesOrderDto(
            h.No,
            h.DocumentType,
            h.SellToCustomerNo,
            h.SellToCustomerName,
            h.PostingDate.ToString("yyyy-MM-dd"),
            h.DueDate?.ToString("yyyy-MM-dd"),
            h.Amount,
            h.AmountIncludingVat,
            h.CurrencyCode,
            h.PaymentTermsCode,
            h.PaymentMethodCode,
            h.SalespersonCode,
            h.ExternalDocumentNo,
            h.Status
        )).ToList();

        return new GetSalesOrdersResult(dtos, total, q.Page, q.PageSize);
    }
}
