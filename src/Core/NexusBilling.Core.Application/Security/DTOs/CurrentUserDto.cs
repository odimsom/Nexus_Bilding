namespace NexusBilling.Core.Application.Security.DTOs;

public record CurrentUserDto(
    Guid Id,
    string Username,
    string Email,
    Guid TenantId,
    bool IsActive
);
