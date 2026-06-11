using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DefaultDimension : Entity
{
    private DefaultDimension() { }

    public TenantIdentifier TenantId { get; private set; }
    public int TableId { get; private set; }
    public string No { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueCode { get; private set; }
    public short ValuePosting { get; private set; }
    public short MultiSelectionAction { get; private set; }

    public static OperationResult<DefaultDimension, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DefaultDimension, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DefaultDimension()
        {
            TenantId = tenantId
        };
        return OperationResult<DefaultDimension, DomainError>.Ok(entity);
    }
}
