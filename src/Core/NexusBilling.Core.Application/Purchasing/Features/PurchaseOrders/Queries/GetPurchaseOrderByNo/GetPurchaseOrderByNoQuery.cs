using MediatR;
using NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrders;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Queries.GetPurchaseOrderByNo;

public record PurchaseOrderLineDetailDto(
    int LineNo,
    string ItemNo,
    string Description,
    string UnitOfMeasure,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    decimal LineDiscountAmount,
    decimal Amount,
    decimal AmountIncludingVat);

public record PurchaseOrderDetailDto(
    string No,
    string VendorNo,
    string VendorName,
    DateTime PostingDate,
    DateTime? DueDate,
    string Status,
    decimal Amount,
    decimal AmountIncludingVat,
    string CurrencyCode,
    string PaymentTermsCode,
    string ExternalDocumentNo,
    List<PurchaseOrderLineDetailDto> Lines);

public record GetPurchaseOrderByNoQuery(Guid TenantId, string No) : IRequest<PurchaseOrderDetailDto?>;
