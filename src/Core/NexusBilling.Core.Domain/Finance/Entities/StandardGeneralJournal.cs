using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class StandardGeneralJournal : Entity
{
    private StandardGeneralJournal() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<StandardGeneralJournal, DomainError> Create(
        TenantIdentifier tenantId,
        string journalTemplateName,
        string code,
        string description)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardGeneralJournal, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalTemplateName))
            return OperationResult<StandardGeneralJournal, DomainError>.Fail(DomainError.Validation("finance.journal_template_name_required", "El campo journal_template_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<StandardGeneralJournal, DomainError>.Fail(DomainError.Validation("finance.code_required", "El campo code es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<StandardGeneralJournal, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));

        var entity = new StandardGeneralJournal()
        {
            TenantId = tenantId,
            JournalTemplateName = journalTemplateName.Trim(),
            Code = code.Trim(),
            Description = description.Trim(),
        };

        return OperationResult<StandardGeneralJournal, DomainError>.Ok(entity);
    }
}
