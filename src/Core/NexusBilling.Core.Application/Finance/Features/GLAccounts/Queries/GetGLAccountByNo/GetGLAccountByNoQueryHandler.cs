using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;
using NexusBilling.Core.Domain.Finance.Repositories;

namespace NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccountByNo;

public sealed class GetGLAccountByNoQueryHandler(
    IGLAccountRepository glAccountRepo)
    : IRequestHandler<GetGLAccountByNoQuery, GLAccountDto?>
{
    public async Task<GLAccountDto?> Handle(GetGLAccountByNoQuery request, CancellationToken cancellationToken)
    {
        var account = await glAccountRepo.GetByNoAsync(request.TenantId, request.No, cancellationToken);
        if (account is null) return null;

        return new GLAccountDto(
            account.No,
            account.Name,
            account.AccountType,
            account.IncomeBalance,
            account.Blocked,
            0m // Default for now
        );
    }
}
