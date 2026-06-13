using MediatR;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Commands.CreateServiceOrder;

public record ServiceOrderLineInput(
    string No,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscountPct,
    string UnitOfMeasure,
    short LineType = 1);

public record CreateServiceOrderCommand(
    Guid TenantId,
    short DocumentType,
    string CustomerNo,
    string CustomerName,
    string Description,
    DateTime OrderDate,
    DateTime? StartingDate,
    DateTime? FinishingDate,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string CurrencyCode,
    string ContractNo,
    string? SeriesCode,
    string? ManualNo,
    IReadOnlyList<ServiceOrderLineInput> Lines) : IRequest<CreateServiceOrderResult>;

public record CreateServiceOrderResult(string No);
