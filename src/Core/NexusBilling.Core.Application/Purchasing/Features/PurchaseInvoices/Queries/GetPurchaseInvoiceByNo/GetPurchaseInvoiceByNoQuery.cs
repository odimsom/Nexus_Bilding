using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Queries.GetPurchaseInvoiceByNo;

public record PurchaseInvoiceDetailDto(
    string No,
    string BuyFromVendorNo,
    string PayToName,
    DateTime PostingDate,
    decimal Amount,
    decimal AmountIncludingVat,
    string CurrencyCode,
    string PaymentTermsCode,
    IReadOnlyList<PurchaseInvoiceLineDto> Lines);

public record PurchaseInvoiceLineDto(
    string Description,
    decimal Quantity,
    decimal UnitCost,
    decimal Amount,
    decimal AmountIncludingVat,
    string UnitOfMeasureCode,
    decimal Vat);

public record GetPurchaseInvoiceByNoQuery(Guid TenantId, string No) : IRequest<PurchaseInvoiceDetailDto?>;
