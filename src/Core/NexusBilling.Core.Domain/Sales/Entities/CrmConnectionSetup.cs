using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CrmConnectionSetup : Entity
{
    private CrmConnectionSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string ServerAddress { get; private set; }
    public string UserName { get; private set; }
    public Guid UserPasswordKey { get; private set; }
    public int LastUpdateInvoiceEntryNo { get; private set; }
    public bool IsEnabled { get; private set; }
    public bool IsUserMappingRequired { get; private set; }
    public bool IsUserMappedToCrmUser { get; private set; }
    public string CrmVersion { get; private set; }
    public bool IsSOrderIntegrationEnabled { get; private set; }
    public bool IsCrmSolutionInstalled { get; private set; }
    public bool IsEnabledForUser { get; private set; }
    public string DynamicsNavUrl { get; private set; }
    public string DynamicsNavOdataUrl { get; private set; }
    public string DynamicsNavOdataUsername { get; private set; }
    public string DynamicsNavOdataAccesskey { get; private set; }
    public Guid DefaultCrmPriceListId { get; private set; }
    public short AuthenticationType { get; private set; }
    public string ConnectionString { get; private set; }
    public string Domain { get; private set; }
    public byte[]? ServerConnectionString { get; private set; }

    public static OperationResult<CrmConnectionSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CrmConnectionSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CrmConnectionSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CrmConnectionSetup, DomainError>.Ok(entity);
    }
}
