using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderLines;

public record PurchaseOrderLineData(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    string LineType = "Item");

public record UpdatePurchaseOrderLinesCommand(
    Guid TenantId,
    string OrderNo,
    IReadOnlyList<PurchaseOrderLineData> Lines) : IRequest<UpdatePurchaseOrderLinesResult>;

public record UpdatePurchaseOrderLinesResult(decimal Amount, decimal AmountIncludingVat);
