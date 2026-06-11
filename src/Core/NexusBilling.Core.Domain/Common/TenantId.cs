using System;
using System.Collections.Generic;

namespace NexusBilling.Core.Domain.Common;

/// <summary>
/// Value Object wrapping a Guid for Tenant Identification.
/// </summary>
public class TenantId : ValueObject
{
    public Guid Value { get; }

    public TenantId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(value));
        }
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(TenantId tenantId) => tenantId.Value;
    public static implicit operator TenantId(Guid value) => new(value);
}
