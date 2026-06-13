using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.CreateSalesOrder;

public record SalesOrderLineInput(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    string LineType = "Item");  // "Item" | "Service" | "G/L Account"

public record CreateSalesOrderCommand(
    Guid TenantId,
    string DocumentType,          // "Order" | "Quote" | "Invoice"
    string SellToCustomerNo,
    string SellToCustomerName,
    string ExternalDocumentNo,
    string CurrencyCode,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    DateTime PostingDate,
    DateTime? DueDate,
    string? SeriesCode,           // null = manual No
    string? ManualNo,             // used when SeriesCode is null
    IReadOnlyList<SalesOrderLineInput> Lines) : IRequest<CreateSalesOrderResult>;

public record CreateSalesOrderResult(string No);
