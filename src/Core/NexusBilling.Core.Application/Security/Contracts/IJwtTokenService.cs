using System.Security.Claims;
using NexusBilling.Core.Application.Security.DTOs;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Core.Application.Security.Contracts;

public interface IJwtTokenService
{
    TokenResponse GenerateToken(User user);
    bool ValidateToken(string token);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
