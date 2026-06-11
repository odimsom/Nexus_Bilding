using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SourceCodeSetup : Entity
{
    private SourceCodeSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string Sales { get; private set; }
    public string Purchases { get; private set; }
    public string InventoryPostCost { get; private set; }
    public string ExchangeRateAdjmt { get; private set; }
    public string PostRecognition { get; private set; }
    public string PostValue { get; private set; }
    public string CloseIncomeStatement { get; private set; }
    public string Consolidation { get; private set; }
    public string GeneralJournal { get; private set; }
    public string SalesJournal { get; private set; }
    public string PurchaseJournal { get; private set; }
    public string CashReceiptJournal { get; private set; }
    public string PaymentJournal { get; private set; }
    public string ItemJournal { get; private set; }
    public string ResourceJournal { get; private set; }
    public string JobJournal { get; private set; }
    public string SalesEntryApplication { get; private set; }
    public string PurchaseEntryApplication { get; private set; }
    public string VatSettlement { get; private set; }
    public string CompressGL { get; private set; }
    public string CompressVatEntries { get; private set; }
    public string CompressCustLedger { get; private set; }
    public string CompressVendLedger { get; private set; }
    public string CompressItemLedger { get; private set; }
    public string CompressResLedger { get; private set; }
    public string CompressJobLedger { get; private set; }
    public string ItemReclassJournal { get; private set; }
    public string PhysInventoryJournal { get; private set; }
    public string CompressBankAccLedger { get; private set; }
    public string CompressCheckLedger { get; private set; }
    public string FinanciallyVoidedCheck { get; private set; }
    public string FinanceChargeMemo { get; private set; }
    public string Reminder { get; private set; }
    public string DeletedDocument { get; private set; }
    public string AdjustAddReportingCurrency { get; private set; }
    public string TransBankRecToGenJnl { get; private set; }
    public string IcGeneralJournal { get; private set; }
    public string UnappliedSalesEntryAppln { get; private set; }
    public string UnappliedPurchEntryAppln { get; private set; }
    public string Reversal { get; private set; }
    public string PaymentReconciliationJournal { get; private set; }
    public string CashFlowWorksheet { get; private set; }
    public string Assembly { get; private set; }
    public string JobGLJournal { get; private set; }
    public string JobGLWip { get; private set; }
    public string GLEntryToCa { get; private set; }
    public string CostJournal { get; private set; }
    public string CostAllocation { get; private set; }
    public string TransferBudgetToActual { get; private set; }
    public string ConsumptionJournal { get; private set; }
    public string OutputJournal { get; private set; }
    public string Flushing { get; private set; }
    public string CapacityJournal { get; private set; }
    public string ProductionJournal { get; private set; }
    public string FixedAssetJournal { get; private set; }
    public string FixedAssetGLJournal { get; private set; }
    public string InsuranceJournal { get; private set; }
    public string CompressFaLedger { get; private set; }
    public string CompressMaintenanceLedger { get; private set; }
    public string CompressInsuranceLedger { get; private set; }
    public string Transfer { get; private set; }
    public string RevaluationJournal { get; private set; }
    public string AdjustCost { get; private set; }
    public string ServiceManagement { get; private set; }
    public string CompressItemBudget { get; private set; }
    public string WhseItemJournal { get; private set; }
    public string WhsePhysInvtJournal { get; private set; }
    public string WhseReclassificationJournal { get; private set; }
    public string WhsePutAway { get; private set; }
    public string WhsePick { get; private set; }
    public string WhseMovement { get; private set; }
    public string CompressWhseEntries { get; private set; }

    public static OperationResult<SourceCodeSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SourceCodeSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SourceCodeSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<SourceCodeSetup, DomainError>.Ok(entity);
    }
}
