using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Commands.CreatePurchaseInvoice;

public record PurchaseInvoiceLineDto(
    string Description,
    decimal Quantity,
    decimal UnitCost,
    string UnitOfMeasureCode,
    decimal VatPct);

public record CreatePurchaseInvoiceCommand(
    Guid TenantId,
    string BuyFromVendorNo,
    string PayToName,
    DateTime PostingDate,
    string? ExternalDocumentNo,
    string? PaymentTermsCode,
    string? CurrencyCode,
    List<PurchaseInvoiceLineDto> Lines) : IRequest<CreatePurchaseInvoiceResult>;

public record CreatePurchaseInvoiceResult(string InvoiceNo);
