using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;

namespace NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccounts;

public record GetGLAccountsQuery(
    Guid TenantId,
    string? Search,
    bool? Blocked,
    int Page,
    int PageSize
) : IRequest<GetGLAccountsResult>;

public record GetGLAccountsResult(
    IReadOnlyList<GLAccountDto> Accounts,
    int TotalItems,
    int TotalPages
);
