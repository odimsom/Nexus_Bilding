using MediatR;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Queries.GetServiceOrderByNo;

public record ServiceLineDto(
    int LineNo,
    short TypeCode,
    string TypeLabel,
    string No,
    string Description,
    string UnitOfMeasure,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineDiscount,
    decimal LineDiscountAmount,
    decimal Amount,
    decimal AmountIncludingVat,
    decimal Vat);

public record ServiceOrderDetailDto(
    string No,
    short DocumentType,
    string DocumentTypeLabel,
    string CustomerNo,
    string CustomerName,
    string Description,
    string? OrderDate,
    string? StartingDate,
    string? FinishingDate,
    string? DueDate,
    string PaymentTermsCode,
    string PaymentMethodCode,
    string SalespersonCode,
    string CurrencyCode,
    string ContractNo,
    short Status,
    string StatusLabel,
    decimal Amount,
    decimal AmountIncludingVat,
    IReadOnlyList<ServiceLineDto> Lines);

public record GetServiceOrderByNoQuery(Guid TenantId, string No) : IRequest<ServiceOrderDetailDto?>;
