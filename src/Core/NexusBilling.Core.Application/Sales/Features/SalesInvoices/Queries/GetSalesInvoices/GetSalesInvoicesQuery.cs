using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoices;

public record SalesInvoiceDto(
    string Id,
    string No,
    string SellToCustomerNo,
    string SellToCustomerName,
    string BillToName,
    DateTime PostingDate,
    DateTime? DueDate,
    string ExternalDocumentNo,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    decimal Amount,
    decimal AmountIncludingVat,
    string Status = "posted");

public record GetSalesInvoicesResult(
    IReadOnlyList<SalesInvoiceDto> Items,
    int TotalCount);

public record GetSalesInvoicesQuery(
    Guid TenantId,
    string? Search,
    int Page = 1,
    int PageSize = 50) : IRequest<GetSalesInvoicesResult>;
