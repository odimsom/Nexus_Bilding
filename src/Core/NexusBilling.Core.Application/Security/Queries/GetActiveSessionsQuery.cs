using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Queries;

public record GetActiveSessionsQuery(Guid UserSid) : IRequest<OperationResult<IEnumerable<ActiveSession>, DomainError>>;

public sealed class GetActiveSessionsQueryHandler : IRequestHandler<GetActiveSessionsQuery, OperationResult<IEnumerable<ActiveSession>, DomainError>>
{
    private readonly IActiveSessionRepository _activeSessionRepository;

    public GetActiveSessionsQueryHandler(IActiveSessionRepository activeSessionRepository)
    {
        _activeSessionRepository = activeSessionRepository;
    }

    public async Task<OperationResult<IEnumerable<ActiveSession>, DomainError>> Handle(GetActiveSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _activeSessionRepository.GetByUserSidAsync(request.UserSid, cancellationToken);
        return OperationResult<IEnumerable<ActiveSession>, DomainError>.Ok(sessions);
    }
}
