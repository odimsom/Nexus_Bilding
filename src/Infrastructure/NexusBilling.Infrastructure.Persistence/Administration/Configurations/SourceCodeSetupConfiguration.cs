using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SourceCodeSetupConfiguration : IEntityTypeConfiguration<SourceCodeSetup>
{
    public void Configure(EntityTypeBuilder<SourceCodeSetup> builder)
    {
        builder.ToTable("source_code_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.Sales).HasColumnName("sales");
        builder.Property(x => x.Purchases).HasColumnName("purchases");
        builder.Property(x => x.InventoryPostCost).HasColumnName("inventory_post_cost");
        builder.Property(x => x.ExchangeRateAdjmt).HasColumnName("exchange_rate_adjmt");
        builder.Property(x => x.PostRecognition).HasColumnName("post_recognition");
        builder.Property(x => x.PostValue).HasColumnName("post_value");
        builder.Property(x => x.CloseIncomeStatement).HasColumnName("close_income_statement");
        builder.Property(x => x.Consolidation).HasColumnName("consolidation");
        builder.Property(x => x.GeneralJournal).HasColumnName("general_journal");
        builder.Property(x => x.SalesJournal).HasColumnName("sales_journal");
        builder.Property(x => x.PurchaseJournal).HasColumnName("purchase_journal");
        builder.Property(x => x.CashReceiptJournal).HasColumnName("cash_receipt_journal");
        builder.Property(x => x.PaymentJournal).HasColumnName("payment_journal");
        builder.Property(x => x.ItemJournal).HasColumnName("item_journal");
        builder.Property(x => x.ResourceJournal).HasColumnName("resource_journal");
        builder.Property(x => x.JobJournal).HasColumnName("job_journal");
        builder.Property(x => x.SalesEntryApplication).HasColumnName("sales_entry_application");
        builder.Property(x => x.PurchaseEntryApplication).HasColumnName("purchase_entry_application");
        builder.Property(x => x.VatSettlement).HasColumnName("vat_settlement");
        builder.Property(x => x.CompressGL).HasColumnName("compress_g_l");
        builder.Property(x => x.CompressVatEntries).HasColumnName("compress_vat_entries");
        builder.Property(x => x.CompressCustLedger).HasColumnName("compress_cust_ledger");
        builder.Property(x => x.CompressVendLedger).HasColumnName("compress_vend_ledger");
        builder.Property(x => x.CompressItemLedger).HasColumnName("compress_item_ledger");
        builder.Property(x => x.CompressResLedger).HasColumnName("compress_res_ledger");
        builder.Property(x => x.CompressJobLedger).HasColumnName("compress_job_ledger");
        builder.Property(x => x.ItemReclassJournal).HasColumnName("item_reclass_journal");
        builder.Property(x => x.PhysInventoryJournal).HasColumnName("phys_inventory_journal");
        builder.Property(x => x.CompressBankAccLedger).HasColumnName("compress_bank_acc_ledger");
        builder.Property(x => x.CompressCheckLedger).HasColumnName("compress_check_ledger");
        builder.Property(x => x.FinanciallyVoidedCheck).HasColumnName("financially_voided_check");
        builder.Property(x => x.FinanceChargeMemo).HasColumnName("finance_charge_memo");
        builder.Property(x => x.Reminder).HasColumnName("reminder");
        builder.Property(x => x.DeletedDocument).HasColumnName("deleted_document");
        builder.Property(x => x.AdjustAddReportingCurrency).HasColumnName("adjust_add_reporting_currency");
        builder.Property(x => x.TransBankRecToGenJnl).HasColumnName("trans_bank_rec_to_gen_jnl");
        builder.Property(x => x.IcGeneralJournal).HasColumnName("ic_general_journal");
        builder.Property(x => x.UnappliedSalesEntryAppln).HasColumnName("unapplied_sales_entry_appln");
        builder.Property(x => x.UnappliedPurchEntryAppln).HasColumnName("unapplied_purch_entry_appln");
        builder.Property(x => x.Reversal).HasColumnName("reversal");
        builder.Property(x => x.PaymentReconciliationJournal).HasColumnName("payment_reconciliation_journal");
        builder.Property(x => x.CashFlowWorksheet).HasColumnName("cash_flow_worksheet");
        builder.Property(x => x.Assembly).HasColumnName("assembly");
        builder.Property(x => x.JobGLJournal).HasColumnName("job_g_l_journal");
        builder.Property(x => x.JobGLWip).HasColumnName("job_g_l_wip");
        builder.Property(x => x.GLEntryToCa).HasColumnName("g_l_entry_to_ca");
        builder.Property(x => x.CostJournal).HasColumnName("cost_journal");
        builder.Property(x => x.CostAllocation).HasColumnName("cost_allocation");
        builder.Property(x => x.TransferBudgetToActual).HasColumnName("transfer_budget_to_actual");
        builder.Property(x => x.ConsumptionJournal).HasColumnName("consumption_journal");
        builder.Property(x => x.OutputJournal).HasColumnName("output_journal");
        builder.Property(x => x.Flushing).HasColumnName("flushing");
        builder.Property(x => x.CapacityJournal).HasColumnName("capacity_journal");
        builder.Property(x => x.ProductionJournal).HasColumnName("production_journal");
        builder.Property(x => x.FixedAssetJournal).HasColumnName("fixed_asset_journal");
        builder.Property(x => x.FixedAssetGLJournal).HasColumnName("fixed_asset_g_l_journal");
        builder.Property(x => x.InsuranceJournal).HasColumnName("insurance_journal");
        builder.Property(x => x.CompressFaLedger).HasColumnName("compress_fa_ledger");
        builder.Property(x => x.CompressMaintenanceLedger).HasColumnName("compress_maintenance_ledger");
        builder.Property(x => x.CompressInsuranceLedger).HasColumnName("compress_insurance_ledger");
        builder.Property(x => x.Transfer).HasColumnName("transfer");
        builder.Property(x => x.RevaluationJournal).HasColumnName("revaluation_journal");
        builder.Property(x => x.AdjustCost).HasColumnName("adjust_cost");
        builder.Property(x => x.ServiceManagement).HasColumnName("service_management");
        builder.Property(x => x.CompressItemBudget).HasColumnName("compress_item_budget");
        builder.Property(x => x.WhseItemJournal).HasColumnName("whse_item_journal");
        builder.Property(x => x.WhsePhysInvtJournal).HasColumnName("whse_phys_invt_journal");
        builder.Property(x => x.WhseReclassificationJournal).HasColumnName("whse_reclassification_journal");
        builder.Property(x => x.WhsePutAway).HasColumnName("whse_put_away");
        builder.Property(x => x.WhsePick).HasColumnName("whse_pick");
        builder.Property(x => x.WhseMovement).HasColumnName("whse_movement");
        builder.Property(x => x.CompressWhseEntries).HasColumnName("compress_whse_entries");
    }
}
