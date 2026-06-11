using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaLedgerEntry : Entity
{
    private FaLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int GLEntryNo { get; private set; }
    public string FaNo { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string DocumentNo { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string Description { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public short FaPostingCategory { get; private set; }
    public short FaPostingType { get; private set; }
    public decimal Amount { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public bool ReclassificationEntry { get; private set; }
    public bool PartOfBookValue { get; private set; }
    public bool PartOfDepreciableBasis { get; private set; }
    public short DisposalCalculationMethod { get; private set; }
    public int DisposalEntryNo { get; private set; }
    public int NoOfDepreciationDays { get; private set; }
    public decimal Quantity { get; private set; }
    public string FaNoBudgetedFaNo { get; private set; }
    public string FaSubclassCode { get; private set; }
    public string FaLocationCode { get; private set; }
    public string FaPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public string UserId { get; private set; }
    public short DepreciationMethod { get; private set; }
    public DateTime? DepreciationStartingDate { get; private set; }
    public decimal StraightLine { get; private set; }
    public decimal NoOfDepreciationYears { get; private set; }
    public decimal FixedDeprAmount { get; private set; }
    public decimal DecliningBalance { get; private set; }
    public string DepreciationTableCode { get; private set; }
    public string JournalBatchName { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public int TransactionNo { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public decimal VatAmount { get; private set; }
    public short GenPostingType { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string FaClassCode { get; private set; }
    public decimal FaExchangeRate { get; private set; }
    public decimal AmountLcy { get; private set; }
    public short ResultOnDisposal { get; private set; }
    public bool Correction { get; private set; }
    public bool IndexEntry { get; private set; }
    public string CanceledFromFaNo { get; private set; }
    public DateTime? DepreciationEndingDate { get; private set; }
    public bool UseFaLedgerCheck { get; private set; }
    public bool AutomaticEntry { get; private set; }
    public DateTime? DeprStartingDateCustom1 { get; private set; }
    public DateTime? DeprEndingDateCustom1 { get; private set; }
    public decimal AccumDeprCustom1 { get; private set; }
    public decimal DeprThisYearCustom1 { get; private set; }
    public short PropertyClassCustom1 { get; private set; }
    public string NoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public bool Reversed { get; private set; }
    public int ReversedByEntryNo { get; private set; }
    public int ReversedEntryNo { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<FaLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaLedgerEntry, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<FaLedgerEntry, DomainError>.Ok(entity);
    }
}
