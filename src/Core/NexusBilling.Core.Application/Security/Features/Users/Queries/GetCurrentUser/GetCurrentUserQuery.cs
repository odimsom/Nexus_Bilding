using MediatR;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Application.Security.Features.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery(Guid UserId)
    : IRequest<OperationResult<CurrentUserDto, DomainError>>;
