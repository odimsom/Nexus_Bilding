using MediatR;

namespace NexusBilling.Core.Application.Security.Commands;

public record UpdateUserCommand(
    Guid TenantId,
    Guid UserId,
    string FullName,
    string Email,
    string EmployeeNo,
    string GroupCode,
    string? NewPassword) : IRequest<bool>;

public record ToggleUserActiveCommand(Guid TenantId, Guid UserId, bool Active) : IRequest<bool>;
