using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLRegister : Entity
{
    private GLRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public string? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }
    public string JournalBatchName { get; private set; }
    public int FromVatEntryNo { get; private set; }
    public int ToVatEntryNo { get; private set; }
    public bool Reversed { get; private set; }

    public static OperationResult<GLRegister, DomainError> Create(
        TenantIdentifier tenantId,
        int no,
        int fromEntryNo,
        int toEntryNo,
        string? creationDate,
        string sourceCode,
        string userId,
        string journalBatchName,
        int fromVatEntryNo,
        int toVatEntryNo,
        bool reversed)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLRegister, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceCode))
            return OperationResult<GLRegister, DomainError>.Fail(DomainError.Validation("finance.source_code_required", "El campo source_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(userId))
            return OperationResult<GLRegister, DomainError>.Fail(DomainError.Validation("finance.user_id_required", "El campo user_id es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalBatchName))
            return OperationResult<GLRegister, DomainError>.Fail(DomainError.Validation("finance.journal_batch_name_required", "El campo journal_batch_name es obligatorio."));

        var entity = new GLRegister()
        {
            TenantId = tenantId,
            No = no,
            FromEntryNo = fromEntryNo,
            ToEntryNo = toEntryNo,
            CreationDate = creationDate,
            SourceCode = sourceCode.Trim(),
            UserId = userId.Trim(),
            JournalBatchName = journalBatchName.Trim(),
            FromVatEntryNo = fromVatEntryNo,
            ToVatEntryNo = toVatEntryNo,
            Reversed = reversed,
        };

        return OperationResult<GLRegister, DomainError>.Ok(entity);
    }
}
