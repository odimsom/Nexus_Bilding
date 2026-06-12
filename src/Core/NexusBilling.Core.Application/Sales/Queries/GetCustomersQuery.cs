using MediatR;
using NexusBilling.Core.Application.Sales.DTOs;

namespace NexusBilling.Core.Application.Sales.Queries;

public record GetCustomersQuery(
    Guid TenantId,
    string? Search,
    bool? Blocked,
    int Page,
    int PageSize
) : IRequest<GetCustomersResult>;

public record GetCustomersResult(
    IReadOnlyList<CustomerListDto> Items,
    int TotalItems,
    int TotalPages
);
