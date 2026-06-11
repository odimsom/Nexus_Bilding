using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaDepreciationBook : Entity
{
    private FaDepreciationBook() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FaNo { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public short DepreciationMethod { get; private set; }
    public DateTime? DepreciationStartingDate { get; private set; }
    public decimal StraightLine { get; private set; }
    public decimal NoOfDepreciationYears { get; private set; }
    public decimal NoOfDepreciationMonths { get; private set; }
    public decimal FixedDeprAmount { get; private set; }
    public decimal DecliningBalance { get; private set; }
    public string DepreciationTableCode { get; private set; }
    public decimal FinalRoundingAmount { get; private set; }
    public decimal EndingBookValue { get; private set; }
    public string FaPostingGroup { get; private set; }
    public DateTime? DepreciationEndingDate { get; private set; }
    public DateTime? AcquisitionDate { get; private set; }
    public DateTime? GLAcquisitionDate { get; private set; }
    public DateTime? DisposalDate { get; private set; }
    public DateTime? LastAcquisitionCostDate { get; private set; }
    public DateTime? LastDepreciationDate { get; private set; }
    public DateTime? LastWriteDownDate { get; private set; }
    public DateTime? LastAppreciationDate { get; private set; }
    public DateTime? LastCustom1Date { get; private set; }
    public DateTime? LastCustom2Date { get; private set; }
    public DateTime? LastSalvageValueDate { get; private set; }
    public decimal FaExchangeRate { get; private set; }
    public decimal FixedDeprAmountBelowZero { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public DateTime? FirstUserDefinedDeprDate { get; private set; }
    public bool UseFaLedgerCheck { get; private set; }
    public DateTime? LastMaintenanceDate { get; private set; }
    public decimal DeprBelowZero { get; private set; }
    public DateTime? ProjectedDisposalDate { get; private set; }
    public decimal ProjectedProceedsOnDisposal { get; private set; }
    public DateTime? DeprStartingDateCustom1 { get; private set; }
    public DateTime? DeprEndingDateCustom1 { get; private set; }
    public decimal AccumDeprCustom1 { get; private set; }
    public decimal DeprThisYearCustom1 { get; private set; }
    public short PropertyClassCustom1 { get; private set; }
    public string Description { get; private set; }
    public short MainAssetComponent { get; private set; }
    public string ComponentOfMainAsset { get; private set; }
    public decimal FaAddCurrencyFactor { get; private set; }
    public bool UseHalfYearConvention { get; private set; }
    public bool UseDbFirstFiscalYear { get; private set; }
    public DateTime? TempEndingDate { get; private set; }
    public decimal TempFixedDeprAmount { get; private set; }
    public bool IgnoreDefEndingBookValue { get; private set; }
    public bool DefaultFaDepreciationBook { get; private set; }

    public static OperationResult<FaDepreciationBook, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaDepreciationBook, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaDepreciationBook()
        {
            TenantId = tenantId
        };
        return OperationResult<FaDepreciationBook, DomainError>.Ok(entity);
    }
}
