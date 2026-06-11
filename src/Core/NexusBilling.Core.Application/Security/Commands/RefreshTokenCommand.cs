using MediatR;
using NexusBilling.Core.Application.Security.Contracts;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Commands;

public record RefreshTokenCommand(string Token, string RefreshToken) 
    : IRequest<OperationResult<TokenResponse, DomainError>>;

public sealed class RefreshTokenCommandHandler 
    : IRequestHandler<RefreshTokenCommand, OperationResult<TokenResponse, DomainError>>
{
    private readonly IActiveSessionRepository _activeSessionRepository;
    private readonly ISessionEventRepository _sessionEventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IActiveSessionRepository activeSessionRepository,
        ISessionEventRepository sessionEventRepository,
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork)
    {
        _activeSessionRepository = activeSessionRepository;
        _sessionEventRepository = sessionEventRepository;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<TokenResponse, DomainError>> Handle(
        RefreshTokenCommand request, 
        CancellationToken cancellationToken)
    {
        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(request.Token);
        if (principal == null)
            return OperationResult<TokenResponse, DomainError>.Fail(DomainError.Business("Auth.InvalidToken", "Invalid access token"));

        var email = principal.Identity?.Name; // O el claim que estemos usando como UniqueName
        // En JwtTokenService.GenerateToken usamos JwtRegisteredClaimNames.UniqueName para user.Username
        // Pero principal.Identity?.Name suele mapear a ese.
        
        var userIdStr = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value 
            ?? principal.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdStr) || !long.TryParse(userIdStr, out var userId))
            return OperationResult<TokenResponse, DomainError>.Fail(DomainError.Business("Auth.InvalidToken", "Invalid token claims"));

        var session = await _activeSessionRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (session == null || session.IsExpired())
            return OperationResult<TokenResponse, DomainError>.Fail(DomainError.Business("Auth.InvalidSession", "Session expired or invalid"));

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken: cancellationToken);
        if (user == null || !user.IsActive)
            return OperationResult<TokenResponse, DomainError>.Fail(DomainError.Business("Auth.UserNotFound", "User not found or inactive"));

        // Rotation
        var newTokenResponse = _jwtTokenService.GenerateToken(user);

        session.Rotate(newTokenResponse.RefreshToken, DateTime.UtcNow.AddDays(7));
        await _activeSessionRepository.UpdateAsync(session, cancellationToken);

        // Audit Refresh Event
        var eventResult = SessionEvent.Create(
            user.TenantId!,
            session.UserSid,
            4, // Custom: Refresh
            user.Username,
            session.SessionUniqueId,
            "Token refreshed successfully");

        if (eventResult.IsSuccess)
            await _sessionEventRepository.AddAsync(eventResult.GetValue(), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return OperationResult<TokenResponse, DomainError>.Ok(newTokenResponse);
    }
}
