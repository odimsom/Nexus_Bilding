using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserPersonalization : Entity
{
    private UserPersonalization() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public string ProfileId { get; private set; }
    public int LanguageId { get; private set; }
    public string Company { get; private set; }
    public bool DebuggerBreakOnError { get; private set; }
    public bool DebuggerBreakOnRecChanges { get; private set; }
    public bool DebuggerSkipSystemTriggers { get; private set; }
    public int LocaleId { get; private set; }
    public string TimeZone { get; private set; }

    public static OperationResult<UserPersonalization, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserPersonalization, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserPersonalization()
        {
            TenantId = tenantId
        };
        return OperationResult<UserPersonalization, DomainError>.Ok(entity);
    }
}
