using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

using Unit = NexusBilling.Core.Domain.Common.Result.Unit;

namespace NexusBilling.Core.Application.Security.Commands;

public record LogoutCommand(string RefreshToken) : IRequest<OperationResult<Unit, DomainError>>;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult<Unit, DomainError>>
{
    private readonly IActiveSessionRepository _activeSessionRepository;
    private readonly ISessionEventRepository _sessionEventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(
        IActiveSessionRepository activeSessionRepository,
        ISessionEventRepository sessionEventRepository,
        IUnitOfWork unitOfWork)
    {
        _activeSessionRepository = activeSessionRepository;
        _sessionEventRepository = sessionEventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<Unit, DomainError>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var session = await _activeSessionRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (session == null)
            return OperationResult<Unit, DomainError>.Ok(Unit.Value);

        // Audit Logout
        var eventResult = SessionEvent.Create(
            session.TenantId,
            session.UserSid,
            1, // Logoff
            session.UserId,
            session.SessionUniqueId,
            "User logged out");

        if (eventResult.IsSuccess)
            await _sessionEventRepository.AddAsync(eventResult.GetValue(), cancellationToken);

        await _activeSessionRepository.DeleteAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<Unit, DomainError>.Ok(Unit.Value);
    }
}
