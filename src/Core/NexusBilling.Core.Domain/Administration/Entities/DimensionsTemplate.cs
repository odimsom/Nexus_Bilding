using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DimensionsTemplate : Entity
{
    private DimensionsTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueCode { get; private set; }
    public short ValuePosting { get; private set; }
    public string Description { get; private set; }
    public int TableId { get; private set; }
    public string MasterRecordTemplateCode { get; private set; }

    public static OperationResult<DimensionsTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DimensionsTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DimensionsTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<DimensionsTemplate, DomainError>.Ok(entity);
    }
}
