using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigQuestionArea : Entity
{
    private ConfigQuestionArea() { }

    public TenantIdentifier TenantId { get; private set; }
    public string QuestionnaireCode { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public int TableId { get; private set; }

    public static OperationResult<ConfigQuestionArea, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigQuestionArea, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigQuestionArea()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigQuestionArea, DomainError>.Ok(entity);
    }
}
