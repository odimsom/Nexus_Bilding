using System;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Common;

public record TenantIdentifier
{
    public Guid Value { get; private init; }

    private TenantIdentifier(Guid value) => Value = value;

    public static TenantIdentifier Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TenantIdentifier cannot be empty.", nameof(value));
        }
        return new TenantIdentifier(value);
    }

    public static implicit operator Guid(TenantIdentifier tenantId) => tenantId.Value;
    public override string ToString() => Value.ToString();
}
