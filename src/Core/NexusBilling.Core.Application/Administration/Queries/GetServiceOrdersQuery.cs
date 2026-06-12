using MediatR;

namespace NexusBilling.Core.Application.Administration.Queries;

public record ServiceOrderListItemDto(
    string No,
    string DocumentType,
    string CustomerNo,
    string CustomerName,
    string Description,
    string? StartingDate,
    string? FinishingDate,
    string? OrderDate,
    short Status,
    string StatusLabel,
    string ContractNo);

public record GetServiceOrdersQuery(
    Guid TenantId,
    string? DocType,
    string? Status,
    string? Search,
    int Page,
    int PageSize) : IRequest<(IReadOnlyList<ServiceOrderListItemDto> Items, int TotalCount)>;
