using MediatR;

namespace NexusBilling.Core.Application.Security.Commands;

public record CreateUserCommand(
    Guid TenantId,
    string Username,
    string Email,
    string Password,
    string FullName,
    string EmployeeNo,
    string GroupCode) : IRequest<Guid>;
