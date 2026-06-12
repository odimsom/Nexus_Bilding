using MediatR;

namespace NexusBilling.Core.Application.Sales.Queries;

public record SalesOrderDto(
    string No,
    string DocumentType,
    string SellToCustomerNo,
    string SellToCustomerName,
    string PostingDate,
    string? DueDate,
    decimal Amount,
    decimal AmountIncludingVat,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string ExternalDocumentNo,
    string Status);

public record GetSalesOrdersQuery(
    Guid TenantId,
    string? DocumentType = null,
    string? Status = null,
    string? Search = null,
    int Page = 1,
    int PageSize = 50) : IRequest<GetSalesOrdersResult>;

public record GetSalesOrdersResult(IReadOnlyList<SalesOrderDto> Items, int TotalCount, int Page, int PageSize);
