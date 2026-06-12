using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Queries;

public record GetPurchaseOrdersQuery(Guid TenantId, int Page, int PageSize) : IRequest<(IReadOnlyList<PurchaseOrderDto> Items, int TotalCount)>;

public record PurchaseOrderDto(
    string No,
    string VendorNo,
    string VendorName,
    DateTime PostingDate,
    string Status,
    decimal Amount,
    decimal AmountIncludingVat,
    string CurrencyCode);
