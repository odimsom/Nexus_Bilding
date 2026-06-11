using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigQuestion : Entity
{
    private ConfigQuestion() { }

    public TenantIdentifier TenantId { get; private set; }
    public string QuestionnaireCode { get; private set; }
    public string QuestionAreaCode { get; private set; }
    public int No { get; private set; }
    public string Question { get; private set; }
    public string AnswerOption { get; private set; }
    public string Answer { get; private set; }
    public int TableId { get; private set; }
    public int FieldId { get; private set; }
    public string Reference { get; private set; }
    public string QuestionOrigin { get; private set; }

    public static OperationResult<ConfigQuestion, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigQuestion, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigQuestion()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigQuestion, DomainError>.Ok(entity);
    }
}
