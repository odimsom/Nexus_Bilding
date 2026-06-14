using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.UpdateQuotationLines;

public record QuotationLineData(
    string? ItemNo,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    decimal VatPct = 18m,
    string LineType = "Item");

public record UpdateQuotationLinesCommand(
    Guid TenantId,
    string QuotationNo,
    IReadOnlyList<QuotationLineData> Lines) : IRequest<UpdateQuotationLinesResult>;

public record UpdateQuotationLinesResult(decimal Amount, decimal AmountIncludingVat);
