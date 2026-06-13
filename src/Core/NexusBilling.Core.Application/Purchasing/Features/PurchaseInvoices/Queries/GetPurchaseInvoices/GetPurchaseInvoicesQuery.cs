using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoices;

public record PurchaseInvoiceDto(
    string Id,
    string No,
    string BuyFromVendorNo,
    string PayToName,
    DateTime PostingDate,
    decimal AmountIncludingVat);

public record GetPurchaseInvoicesResult(
    IReadOnlyList<PurchaseInvoiceDto> Items,
    int TotalCount);

public record GetPurchaseInvoicesQuery(
    Guid TenantId,
    string? Search,
    int Page = 1,
    int PageSize = 50) : IRequest<GetPurchaseInvoicesResult>;
