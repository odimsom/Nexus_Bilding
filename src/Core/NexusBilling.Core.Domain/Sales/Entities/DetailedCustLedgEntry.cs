using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class DetailedCustLedgEntry : Entity
{
    private DetailedCustLedgEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int CustLedgerEntryNo { get; private set; }
    public short EntryType { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string CustomerNo { get; private set; }
    public string CurrencyCode { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public int TransactionNo { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal DebitAmountLcy { get; private set; }
    public decimal CreditAmountLcy { get; private set; }
    public DateTime? InitialEntryDueDate { get; private set; }
    public string InitialEntryGlobalDim1 { get; private set; }
    public string InitialEntryGlobalDim2 { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public short InitialDocumentType { get; private set; }
    public int AppliedCustLedgerEntryNo { get; private set; }
    public bool Unapplied { get; private set; }
    public int UnappliedByEntryNo { get; private set; }
    public decimal RemainingPmtDiscPossible { get; private set; }
    public decimal MaxPaymentTolerance { get; private set; }
    public string TaxJurisdictionCode { get; private set; }
    public int ApplicationNo { get; private set; }
    public bool LedgerEntryAmount { get; private set; }

    public static OperationResult<DetailedCustLedgEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DetailedCustLedgEntry, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DetailedCustLedgEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<DetailedCustLedgEntry, DomainError>.Ok(entity);
    }
}
