using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobsSetup : Entity
{
    private JobsSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string JobNos { get; private set; }
    public bool ApplyUsageLinkByDefault { get; private set; }
    public string DefaultWipMethod { get; private set; }
    public string DefaultJobPostingGroup { get; private set; }
    public short DefaultWipPostingMethod { get; private set; }
    public bool AllowSchedContractLinesDef { get; private set; }
    public short LogoPositionOnDocuments { get; private set; }
    public string JobWipNos { get; private set; }
    public bool AutomaticUpdateJobItemCost { get; private set; }

    public static OperationResult<JobsSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobsSetup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobsSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<JobsSetup, DomainError>.Ok(entity);
    }
}
