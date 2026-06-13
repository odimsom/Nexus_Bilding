using MediatR;

namespace NexusBilling.Core.Application.Security.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    Guid TenantId,
    string Username,
    string Email,
    string Password,
    string FullName,
    string EmployeeNo,
    string GroupCode) : IRequest<Guid>;
