using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Domain.Administration.Entities;

/// <summary>
/// Tenant Aggregate Root.
/// </summary>
public class Tenant : Entity
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
