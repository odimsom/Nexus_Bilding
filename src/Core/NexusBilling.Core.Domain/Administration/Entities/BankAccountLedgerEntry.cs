using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankAccountLedgerEntry : Entity
{
    private BankAccountLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string BankAccountNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal Amount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string BankAccPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string OurContactCode { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public bool Open { get; private set; }
    public bool Positive { get; private set; }
    public int ClosedByEntryNo { get; private set; }
    public DateTime? ClosedAtDate { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public int TransactionNo { get; private set; }
    public short StatementStatus { get; private set; }
    public string StatementNo { get; private set; }
    public int StatementLineNo { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal DebitAmountLcy { get; private set; }
    public decimal CreditAmountLcy { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public bool Reversed { get; private set; }
    public int ReversedByEntryNo { get; private set; }
    public int ReversedEntryNo { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<BankAccountLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankAccountLedgerEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankAccountLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<BankAccountLedgerEntry, DomainError>.Ok(entity);
    }
}
