using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaSetup : Entity
{
    private FaSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public bool AllowPostingToMainAssets { get; private set; }
    public string DefaultDeprBook { get; private set; }
    public DateTime? AllowFaPostingFrom { get; private set; }
    public DateTime? AllowFaPostingTo { get; private set; }
    public string InsuranceDeprBook { get; private set; }
    public bool AutomaticInsurancePosting { get; private set; }
    public string FixedAssetNos { get; private set; }
    public string InsuranceNos { get; private set; }

    public static OperationResult<FaSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<FaSetup, DomainError>.Ok(entity);
    }
}
