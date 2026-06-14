using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.ConvertQuotationToOrder;

public record ConvertQuotationToOrderCommand(
    Guid TenantId,
    string QuotationNo,
    string? SeriesCode) : IRequest<ConvertQuotationToOrderResult>;

public record ConvertQuotationToOrderResult(string OrderNo);
