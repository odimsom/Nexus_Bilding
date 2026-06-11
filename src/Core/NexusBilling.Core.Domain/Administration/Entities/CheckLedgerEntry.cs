using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CheckLedgerEntry : Entity
{
    private CheckLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string BankAccountNo { get; private set; }
    public int BankAccountLedgerEntryNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime? CheckDate { get; private set; }
    public string CheckNo { get; private set; }
    public short CheckType { get; private set; }
    public short BankPaymentType { get; private set; }
    public short EntryStatus { get; private set; }
    public short OriginalEntryStatus { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public bool Open { get; private set; }
    public short StatementStatus { get; private set; }
    public string StatementNo { get; private set; }
    public int StatementLineNo { get; private set; }
    public string UserId { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public int DataExchEntryNo { get; private set; }
    public int DataExchVoidedEntryNo { get; private set; }
    public bool PositivePayExported { get; private set; }
    public string RecordIdToPrint { get; private set; }

    public static OperationResult<CheckLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CheckLedgerEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CheckLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<CheckLedgerEntry, DomainError>.Ok(entity);
    }
}
