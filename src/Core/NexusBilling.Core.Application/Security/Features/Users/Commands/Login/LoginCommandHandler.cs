using MediatR;
using NexusBilling.Core.Application.Security.Contracts;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Features.Users.Commands.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, OperationResult<TokenResponse, DomainError>>
{
    private readonly IUserRepository _userRepository;
    private readonly IActiveSessionRepository _activeSessionRepository;
    private readonly ISessionEventRepository _sessionEventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IActiveSessionRepository activeSessionRepository,
        ISessionEventRepository sessionEventRepository,
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _activeSessionRepository = activeSessionRepository;
        _sessionEventRepository = sessionEventRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<OperationResult<TokenResponse, DomainError>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return OperationResult<TokenResponse, DomainError>.Fail(
                DomainError.Business("Auth.InvalidCredentials", "Invalid email or password."));

        if (!user.IsActive)
            return OperationResult<TokenResponse, DomainError>.Fail(
                DomainError.Business("Auth.UserInactive", "User account is inactive."));

        var passwordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return OperationResult<TokenResponse, DomainError>.Fail(
                DomainError.Business("Auth.InvalidCredentials", "Invalid email or password."));

        var tokenResponse = _jwtTokenService.GenerateToken(user);

        // Crear Sesión Activa (Server-side refresh token)
        var sessionResult = ActiveSession.Create(
            user.TenantId!,
            Guid.Empty, // NAV User SID - we can use user.Id or empty for now
            user.Username,
            tokenResponse.RefreshToken,
            DateTime.UtcNow.AddDays(7)); // Refresh token dura 7 días

        if (!sessionResult.IsSuccess)
            return OperationResult<TokenResponse, DomainError>.Fail(sessionResult.GetError());

        var session = sessionResult.GetValue();
        await _activeSessionRepository.AddAsync(session, cancellationToken);

        // Crear Evento de Sesión (Auditoría)
        var eventResult = SessionEvent.Create(
            user.TenantId!,
            Guid.Empty,
            0, // Logon
            user.Username,
            session.SessionUniqueId,
            "User login successful");

        if (eventResult.IsSuccess)
            await _sessionEventRepository.AddAsync(eventResult.GetValue(), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<TokenResponse, DomainError>.Ok(tokenResponse);
    }
}
