using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;

namespace NexusBilling.Core.Application.Finance.Features.GLAccounts.Queries.GetGLAccountByNo;

public record GetGLAccountByNoQuery(
    Guid TenantId,
    string No
) : IRequest<GLAccountDto?>;
