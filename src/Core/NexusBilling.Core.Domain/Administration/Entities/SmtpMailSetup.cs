using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SmtpMailSetup : Entity
{
    private SmtpMailSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string SmtpServer { get; private set; }
    public short Authentication { get; private set; }
    public string UserId { get; private set; }
    public int SmtpServerPort { get; private set; }
    public bool SecureConnection { get; private set; }
    public Guid PasswordKey { get; private set; }

    public static OperationResult<SmtpMailSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SmtpMailSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SmtpMailSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<SmtpMailSetup, DomainError>.Ok(entity);
    }
}
