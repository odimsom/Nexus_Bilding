namespace NexusBilling.Core.Application.Security.DTOs;

public record TokenResponse(
    string Token,
    string RefreshToken,
    DateTime ExpiresAt,
    string Username,
    string Email,
    Guid TenantId
);
