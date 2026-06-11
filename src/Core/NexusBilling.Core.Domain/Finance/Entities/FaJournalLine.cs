using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaJournalLine : Entity
{
    private FaJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int LineNo { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public short FaPostingType { get; private set; }
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
    public decimal SalvageValue { get; private set; }
    public decimal Quantity { get; private set; }
    public bool Correction { get; private set; }
    public int NoOfDepreciationDays { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public bool DeprAcquisitionCost { get; private set; }
    public string FaPostingGroup { get; private set; }
    public string MaintenanceCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string InsuranceNo { get; private set; }
    public string BudgetedFaNo { get; private set; }
    public bool UseDuplicationList { get; private set; }
    public string DuplicateInDepreciationBook { get; private set; }
    public bool FaReclassificationEntry { get; private set; }
    public int FaErrorEntryNo { get; private set; }
    public string ReasonCode { get; private set; }
    public string SourceCode { get; private set; }
    public short RecurringMethod { get; private set; }
    public string RecurringFrequency { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool IndexEntry { get; private set; }
    public string PostingNoSeries { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<FaJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaJournalLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<FaJournalLine, DomainError>.Ok(entity);
    }
}
