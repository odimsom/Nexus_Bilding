using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TransformationRule : Entity
{
    private TransformationRule() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short TransformationType { get; private set; }
    public string FindValue { get; private set; }
    public string ReplaceValue { get; private set; }
    public string StartingText { get; private set; }
    public string EndingText { get; private set; }
    public int StartPosition { get; private set; }
    public int Length { get; private set; }
    public string DataFormat { get; private set; }
    public string DataFormattingCulture { get; private set; }
    public string NextTransformationRule { get; private set; }

    public static OperationResult<TransformationRule, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TransformationRule, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TransformationRule()
        {
            TenantId = tenantId
        };
        return OperationResult<TransformationRule, DomainError>.Ok(entity);
    }
}
