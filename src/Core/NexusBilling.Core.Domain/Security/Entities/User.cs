using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Domain.Security.Entities;

/// <summary>
/// User Aggregate Root.
/// </summary>
public class User : Entity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public TenantIdentifier TenantId { get; set; } = null!;
}
