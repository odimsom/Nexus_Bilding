using MediatR;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.CreatePurchaseOrder;

public record PurchaseOrderLineDto(
    string LineType,
    string? ItemNo,
    string Description,
    string UnitOfMeasure,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct);

public record CreatePurchaseOrderCommand(
    Guid TenantId,
    string? ManualNo,
    string? SeriesCode,
    string BuyFromVendorNo,
    string PayToName,
    DateTime PostingDate,
    DateTime? DueDate,
    string? CurrencyCode,
    string? PaymentTermsCode,
    string? ExternalDocumentNo,
    List<PurchaseOrderLineDto> Lines) : IRequest<CreatePurchaseOrderResult>;

public record CreatePurchaseOrderResult(string No);
