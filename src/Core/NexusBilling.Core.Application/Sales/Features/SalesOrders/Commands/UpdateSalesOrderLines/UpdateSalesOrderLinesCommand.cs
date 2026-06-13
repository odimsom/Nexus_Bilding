using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.UpdateSalesOrderLines;

public record SalesOrderLineData(
    string ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    string LineType = "Item");

public record UpdateSalesOrderLinesCommand(
    Guid TenantId,
    string OrderNo,
    IReadOnlyList<SalesOrderLineData> Lines) : IRequest<UpdateSalesOrderLinesResult>;

public record UpdateSalesOrderLinesResult(decimal Amount, decimal AmountIncludingVat);
