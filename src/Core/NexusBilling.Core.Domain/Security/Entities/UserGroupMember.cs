using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserGroupMember : Entity
{
    private UserGroupMember() { }

    public TenantIdentifier TenantId { get; set; }
    public string UserGroupCode { get; set; } = string.Empty;
    public Guid UserSecurityId { get; set; }
    public string CompanyName { get; set; } = string.Empty;

    public static UserGroupMember Create(TenantIdentifier tenantId, string groupCode, Guid userSecurityId)
        => new() { TenantId = tenantId, UserGroupCode = groupCode, UserSecurityId = userSecurityId, CompanyName = string.Empty };
}
