using MediatR;

namespace NexusBilling.Core.Application.Security.Queries;

public record UserDto(
    Guid Id,
    string Username,
    string Email,
    string FullName,
    string EmployeeNo,
    bool IsActive,
    string GroupCode,
    string GroupName);

public record GetUsersQuery(Guid TenantId) : IRequest<IReadOnlyList<UserDto>>;
