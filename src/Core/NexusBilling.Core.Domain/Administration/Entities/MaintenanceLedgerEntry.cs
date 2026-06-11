using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MaintenanceLedgerEntry : Entity
{
    private MaintenanceLedgerEntry() { }

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
    public decimal Amount { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal Quantity { get; private set; }
    public string FaNoBudgetedFaNo { get; private set; }
    public string FaSubclassCode { get; private set; }
    public string FaLocationCode { get; private set; }
    public string FaPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string LocationCode { get; private set; }
    public string UserId { get; private set; }
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
    public string DepreciationBookCode { get; private set; }
    public decimal FaExchangeRate { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string MaintenanceCode { get; private set; }
    public bool Correction { get; private set; }
    public bool IndexEntry { get; private set; }
    public bool AutomaticEntry { get; private set; }
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

    public static OperationResult<MaintenanceLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MaintenanceLedgerEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MaintenanceLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<MaintenanceLedgerEntry, DomainError>.Ok(entity);
    }
}
