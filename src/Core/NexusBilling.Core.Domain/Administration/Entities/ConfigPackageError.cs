using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageError : Entity
{
    private ConfigPackageError() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int RecordNo { get; private set; }
    public int FieldId { get; private set; }
    public string ErrorText { get; private set; }
    public short ErrorType { get; private set; }
    public string RecordId { get; private set; }

    public static OperationResult<ConfigPackageError, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageError, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageError()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageError, DomainError>.Ok(entity);
    }
}
