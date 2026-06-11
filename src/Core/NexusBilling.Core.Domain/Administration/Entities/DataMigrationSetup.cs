using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DataMigrationSetup : Entity
{
    private DataMigrationSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string DefaultCustomerTemplate { get; private set; }
    public string DefaultVendorTemplate { get; private set; }
    public string DefaultItemTemplate { get; private set; }
    public string DefaultAccountTemplate { get; private set; }
    public string DefaultPostingGroupTemplate { get; private set; }
    public string DefaultCustPostingTemplate { get; private set; }
    public string DefaultVendPostingTemplate { get; private set; }

    public static OperationResult<DataMigrationSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DataMigrationSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DataMigrationSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<DataMigrationSetup, DomainError>.Ok(entity);
    }
}
