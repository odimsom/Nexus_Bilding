using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class DepreciationBook : Entity
{
    private DepreciationBook() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool GLIntegrationAcqCost { get; private set; }
    public bool GLIntegrationDepreciation { get; private set; }
    public bool GLIntegrationWriteDown { get; private set; }
    public bool GLIntegrationAppreciation { get; private set; }
    public bool GLIntegrationCustom1 { get; private set; }
    public bool GLIntegrationCustom2 { get; private set; }
    public bool GLIntegrationDisposal { get; private set; }
    public bool GLIntegrationMaintenance { get; private set; }
    public short DisposalCalculationMethod { get; private set; }
    public bool UseCustom1Depreciation { get; private set; }
    public bool AllowDeprBelowZero { get; private set; }
    public bool UseFaExchRateInDuplic { get; private set; }
    public bool PartOfDuplicationList { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public bool AllowIndexation { get; private set; }
    public bool UseSameFaGLPostingDates { get; private set; }
    public decimal DefaultExchangeRate { get; private set; }
    public bool UseFaLedgerCheck { get; private set; }
    public bool UseRoundingInPeriodicDepr { get; private set; }
    public DateTime? NewFiscalYearStartingDate { get; private set; }
    public int NoOfDaysInFiscalYear { get; private set; }
    public bool AllowChangesInDeprFields { get; private set; }
    public decimal DefaultFinalRoundingAmount { get; private set; }
    public decimal DefaultEndingBookValue { get; private set; }
    public short PeriodicDeprDateCalc { get; private set; }
    public bool MarkErrorsAsCorrections { get; private set; }
    public bool AddCurrExchRateAcqCost { get; private set; }
    public bool AddCurrExchRateDepr { get; private set; }
    public bool AddCurrExchRateWriteDown { get; private set; }
    public bool AddCurrExchRateApprec { get; private set; }
    public bool AddCurrExchRateCustom1 { get; private set; }
    public bool AddCurrExchRateCustom2 { get; private set; }
    public bool AddCurrExchRateDisp { get; private set; }
    public bool AddCurrExchRateMaint { get; private set; }
    public bool UseDefaultDimension { get; private set; }
    public bool SubtractDiscInPurchInv { get; private set; }
    public bool AllowCorrectionOfDisposal { get; private set; }
    public bool AllowMoreThan360365Days { get; private set; }
    public bool VatOnNetDisposalEntries { get; private set; }
    public bool AllowAcqCostBelowZero { get; private set; }
    public bool AllowIdenticalDocumentNo { get; private set; }
    public bool FiscalYear365Days { get; private set; }

    public static OperationResult<DepreciationBook, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DepreciationBook, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DepreciationBook()
        {
            TenantId = tenantId
        };
        return OperationResult<DepreciationBook, DomainError>.Ok(entity);
    }
}
