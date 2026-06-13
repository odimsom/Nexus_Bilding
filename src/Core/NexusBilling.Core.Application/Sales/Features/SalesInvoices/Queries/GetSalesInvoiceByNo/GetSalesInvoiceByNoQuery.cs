using MediatR;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Queries.GetSalesOrderByNo;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoiceByNo;

public record SalesInvoiceDetailDto(
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
    string OrderNo,
    decimal Amount,
    decimal AmountIncludingVat,
    IReadOnlyList<SalesInvoiceLineDto> Lines);

public record SalesInvoiceLineDto(
    int LineNo,
    string Type,
    string No,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    decimal Amount,
    decimal AmountIncludingVat,
    string UnitOfMeasureCode,
    decimal Vat);

public record GetSalesInvoiceByNoQuery(Guid TenantId, string No) : IRequest<SalesInvoiceDetailDto?>;
