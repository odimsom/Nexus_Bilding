using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FaultResolCodRelationship : Entity
{
    private FaultResolCodRelationship() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FaultCode { get; private set; }
    public string SymptomCode { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string ResolutionCode { get; private set; }
    public int Occurrences { get; private set; }
    public string Description { get; private set; }
    public string ServiceItemGroupCode { get; private set; }
    public bool CreatedManually { get; private set; }

    public static OperationResult<FaultResolCodRelationship, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaultResolCodRelationship, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaultResolCodRelationship()
        {
            TenantId = tenantId
        };
        return OperationResult<FaultResolCodRelationship, DomainError>.Ok(entity);
    }
}
