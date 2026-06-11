using MediatR;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Queries;

public sealed class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, OperationResult<CurrentUserDto, DomainError>>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<OperationResult<CurrentUserDto, DomainError>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.FindAsync(
            u => u.Id == request.UserId,
            cancellationToken: cancellationToken);

        var user = users.FirstOrDefault();

        if (user is null)
            return OperationResult<CurrentUserDto, DomainError>.Fail(
                DomainError.NotFound("User.NotFound", $"User with id {request.UserId} was not found."));

        var dto = new CurrentUserDto(
            user.Id,
            user.Username,
            user.Email,
            user.TenantId?.Value ?? Guid.Empty,
            user.IsActive);

        return OperationResult<CurrentUserDto, DomainError>.Ok(dto);
    }
}
