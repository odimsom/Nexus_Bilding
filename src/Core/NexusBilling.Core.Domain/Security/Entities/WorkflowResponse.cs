using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowResponse : Entity
{
    private WorkflowResponse() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FunctionName { get; private set; }
    public int TableId { get; private set; }
    public string Description { get; private set; }
    public string ResponseOptionGroup { get; private set; }
    public bool Independent { get; private set; }

    public static OperationResult<WorkflowResponse, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowResponse, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowResponse()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowResponse, DomainError>.Ok(entity);
    }
}
