using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigPackageField : Entity
{
    private ConfigPackageField() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PackageCode { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }
    public string FieldName { get; private set; }
    public string FieldCaption { get; private set; }
    public bool ValidateField { get; private set; }
    public bool IncludeField { get; private set; }
    public bool LocalizeField { get; private set; }
    public int RelationTableId { get; private set; }
    public bool Dimension { get; private set; }
    public bool PrimaryKey { get; private set; }
    public int ProcessingOrder { get; private set; }
    public bool CreateMissingCodes { get; private set; }

    public static OperationResult<ConfigPackageField, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigPackageField, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigPackageField()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigPackageField, DomainError>.Ok(entity);
    }
}
