using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobTask : Entity
{
    private JobTask() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public string Description { get; private set; }
    public short JobTaskType { get; private set; }
    public short WipTotal { get; private set; }
    public string JobPostingGroup { get; private set; }
    public string WipMethod { get; private set; }
    public string Totaling { get; private set; }
    public bool NewPage { get; private set; }
    public int NoOfBlankLines { get; private set; }
    public int Indentation { get; private set; }
    public decimal RecognizedSalesAmount { get; private set; }
    public decimal RecognizedCostsAmount { get; private set; }
    public decimal RecognizedSalesGLAmount { get; private set; }
    public decimal RecognizedCostsGLAmount { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }

    public static OperationResult<JobTask, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobTask, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobTask()
        {
            TenantId = tenantId
        };
        return OperationResult<JobTask, DomainError>.Ok(entity);
    }
}
