using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Queries.GetSalesOrderByNo;

public record SalesLineDto(
    int LineNo,
    string Type,
    string No,
    string Description,
    string UnitOfMeasure,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscount,
    decimal LineDiscountAmount,
    decimal Amount,
    decimal AmountIncludingVat,
    decimal Vat);

public record SalesOrderDetailDto(
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
    string Status,
    IReadOnlyList<SalesLineDto> Lines);

public record GetSalesOrderByNoQuery(Guid TenantId, string No) : IRequest<SalesOrderDetailDto?>;
