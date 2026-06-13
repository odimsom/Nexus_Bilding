using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;
using NexusBilling.Core.Domain.Finance.Repositories;

namespace NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccounts;

public sealed class GetGLAccountsQueryHandler(
    IGLAccountRepository glAccountRepo)
    : IRequestHandler<GetGLAccountsQuery, GetGLAccountsResult>
{
    public async Task<GetGLAccountsResult> Handle(GetGLAccountsQuery request, CancellationToken cancellationToken)
    {
        var (accounts, total) = await glAccountRepo.ListAsync(
            request.TenantId, request.Search, request.Blocked, request.Page, request.PageSize, cancellationToken);

        var dtos = accounts.Select(a => new GLAccountDto(
            a.No,
            a.Name,
            a.AccountType,
            a.IncomeBalance,
            a.Blocked,
            0m // Default for now, later we'd calculate balance from GLEntries
        )).ToList();

        var totalPages = (int)Math.Ceiling(total / (double)request.PageSize);

        return new GetGLAccountsResult(dtos, total, totalPages);
    }
}
