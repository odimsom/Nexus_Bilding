using MediatR;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotations;

public sealed class GetQuotationsQueryHandler(ISalesHeaderRepository repo)
    : IRequestHandler<GetQuotationsQuery, GetQuotationsResult>
{
    public async Task<GetQuotationsResult> Handle(GetQuotationsQuery q, CancellationToken ct)
    {
        string? search = !string.IsNullOrWhiteSpace(q.CustomerNo) ? q.CustomerNo : q.Search;

        var (items, total) = await repo.GetPagedForTenantAsync(
            q.TenantId, "Quote", q.Status, search, q.Page, q.PageSize, ct);

        // Filter by customerNo if explicitly passed (overrides free-text search)
        IEnumerable<NexusBilling.Core.Domain.Sales.Entities.SalesHeader> filtered = items;
        if (!string.IsNullOrWhiteSpace(q.CustomerNo))
            filtered = items.Where(x => x.SellToCustomerNo == q.CustomerNo);

        var now = DateTime.UtcNow;
        var result = filtered.Select(x => new QuotationListItem(
            x.No,
            x.SellToCustomerNo,
            x.SellToCustomerName,
            x.PostingDate.ToString("yyyy-MM-dd"),
            x.ValidUntilDate?.ToString("yyyy-MM-dd"),
            x.QuotedBy,
            x.Status,
            x.Amount,
            x.AmountIncludingVat,
            x.ValidUntilDate.HasValue && x.ValidUntilDate.Value < now
        )).ToList();

        return new GetQuotationsResult(result, total, q.Page, q.PageSize);
    }
}
