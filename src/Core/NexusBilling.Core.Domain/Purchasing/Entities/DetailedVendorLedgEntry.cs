using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class DetailedVendorLedgEntry : Entity
{
    private DetailedVendorLedgEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int VendorLedgerEntryNo { get; private set; }
    public short EntryType { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string VendorNo { get; private set; }
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
    public int AppliedVendLedgerEntryNo { get; private set; }
    public bool Unapplied { get; private set; }
    public int UnappliedByEntryNo { get; private set; }
    public decimal RemainingPmtDiscPossible { get; private set; }
    public decimal MaxPaymentTolerance { get; private set; }
    public string TaxJurisdictionCode { get; private set; }
    public int ApplicationNo { get; private set; }
    public bool LedgerEntryAmount { get; private set; }

    public static OperationResult<DetailedVendorLedgEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DetailedVendorLedgEntry, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DetailedVendorLedgEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<DetailedVendorLedgEntry, DomainError>.Ok(entity);
    }
}
