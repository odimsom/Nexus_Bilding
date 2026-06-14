using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Queries.GetQuotations;

public record GetQuotationsQuery(
    Guid TenantId,
    string? CustomerNo,
    string? Search,
    string? Status,
    int Page = 1,
    int PageSize = 50) : IRequest<GetQuotationsResult>;

public record QuotationListItem(
    string No,
    string SellToCustomerNo,
    string SellToCustomerName,
    string PostingDate,
    string? ValidUntilDate,
    string? QuotedBy,
    string Status,
    decimal Amount,
    decimal AmountIncludingVat,
    bool IsExpired);

public record GetQuotationsResult(
    IReadOnlyList<QuotationListItem> Items,
    int TotalCount,
    int Page,
    int PageSize);
