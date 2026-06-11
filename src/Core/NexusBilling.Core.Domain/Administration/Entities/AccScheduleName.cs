using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccScheduleName : Entity
{
    private AccScheduleName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string DefaultColumnLayout { get; private set; }
    public string AnalysisViewName { get; private set; }

    public static OperationResult<AccScheduleName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccScheduleName, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccScheduleName()
        {
            TenantId = tenantId
        };
        return OperationResult<AccScheduleName, DomainError>.Ok(entity);
    }
}
