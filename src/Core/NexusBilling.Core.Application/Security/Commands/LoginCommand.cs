using MediatR;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Application.Security.Commands;

public record LoginCommand(string Email, string Password)
    : IRequest<OperationResult<TokenResponse, DomainError>>;
