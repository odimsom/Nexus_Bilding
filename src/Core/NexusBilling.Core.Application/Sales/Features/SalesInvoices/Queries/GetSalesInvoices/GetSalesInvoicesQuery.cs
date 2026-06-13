using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoices;

public record SalesInvoiceDto(
    string Id,
    string No,
    string SellToCustomerNo,
    string BillToName,
    DateTime PostingDate,
    decimal AmountIncludingVat);

public record GetSalesInvoicesResult(
    IReadOnlyList<SalesInvoiceDto> Items,
    int TotalCount);

public record GetSalesInvoicesQuery(
    Guid TenantId,
    string? Search,
    int Page = 1,
    int PageSize = 50) : IRequest<GetSalesInvoicesResult>;
