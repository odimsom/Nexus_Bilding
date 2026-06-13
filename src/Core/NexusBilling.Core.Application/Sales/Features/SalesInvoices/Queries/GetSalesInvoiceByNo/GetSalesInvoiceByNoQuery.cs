using MediatR;
using NexusBilling.Core.Application.Sales.Features.SalesOrders.Queries.GetSalesOrderByNo;

namespace NexusBilling.Core.Application.Sales.Features.SalesInvoices.Queries.GetSalesInvoiceByNo;

public record SalesInvoiceDetailDto(
    string No,
    string SellToCustomerNo,
    string BillToName,
    DateTime PostingDate,
    decimal Amount,
    decimal AmountIncludingVat,
    string CurrencyCode,
    string PaymentTermsCode,
    IReadOnlyList<SalesInvoiceLineDto> Lines);

public record SalesInvoiceLineDto(
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal Amount,
    decimal AmountIncludingVat,
    string UnitOfMeasureCode,
    decimal Vat);

public record GetSalesInvoiceByNoQuery(Guid TenantId, string No) : IRequest<SalesInvoiceDetailDto?>;
