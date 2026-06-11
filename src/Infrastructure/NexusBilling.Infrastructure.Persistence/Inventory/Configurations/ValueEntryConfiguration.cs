using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ValueEntryConfiguration : IEntityTypeConfiguration<ValueEntry>
{
    public void Configure(EntityTypeBuilder<ValueEntry> builder)
    {
        builder.ToTable("value_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.ItemLedgerEntryType).HasColumnName("item_ledger_entry_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.SourcePostingGroup).HasColumnName("source_posting_group");
        builder.Property(x => x.ItemLedgerEntryNo).HasColumnName("item_ledger_entry_no");
        builder.Property(x => x.ValuedQuantity).HasColumnName("valued_quantity").HasPrecision(18, 5);
        builder.Property(x => x.ItemLedgerEntryQuantity).HasColumnName("item_ledger_entry_quantity").HasPrecision(18, 5);
        builder.Property(x => x.InvoicedQuantity).HasColumnName("invoiced_quantity").HasPrecision(18, 5);
        builder.Property(x => x.CostPerUnit).HasColumnName("cost_per_unit").HasPrecision(18, 5);
        builder.Property(x => x.SalesAmountActual).HasColumnName("sales_amount_actual").HasPrecision(18, 5);
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount").HasPrecision(18, 5);
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.AppliesToEntry).HasColumnName("applies_to_entry");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.CostAmountActual).HasColumnName("cost_amount_actual").HasPrecision(18, 5);
        builder.Property(x => x.CostPostedToGL).HasColumnName("cost_posted_to_g_l").HasPrecision(18, 5);
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.DropShipment).HasColumnName("drop_shipment");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.CostAmountActualAcy).HasColumnName("cost_amount_actual_acy").HasPrecision(18, 5);
        builder.Property(x => x.CostPostedToGLAcy).HasColumnName("cost_posted_to_g_l_acy").HasPrecision(18, 5);
        builder.Property(x => x.CostPerUnitAcy).HasColumnName("cost_per_unit_acy").HasPrecision(18, 5);
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentLineNo).HasColumnName("document_line_no");
        builder.Property(x => x.OrderType).HasColumnName("order_type");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.ExpectedCost).HasColumnName("expected_cost");
        builder.Property(x => x.ItemChargeNo).HasColumnName("item_charge_no");
        builder.Property(x => x.ValuedByAverageCost).HasColumnName("valued_by_average_cost");
        builder.Property(x => x.PartialRevaluation).HasColumnName("partial_revaluation");
        builder.Property(x => x.Inventoriable).HasColumnName("inventoriable");
        builder.Property(x => x.ValuationDate).HasColumnName("valuation_date");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.VarianceType).HasColumnName("variance_type");
        builder.Property(x => x.PurchaseAmountActual).HasColumnName("purchase_amount_actual").HasPrecision(18, 5);
        builder.Property(x => x.PurchaseAmountExpected).HasColumnName("purchase_amount_expected").HasPrecision(18, 5);
        builder.Property(x => x.SalesAmountExpected).HasColumnName("sales_amount_expected").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountExpected).HasColumnName("cost_amount_expected").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountNonInvtbl).HasColumnName("cost_amount_non_invtbl").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountExpectedAcy).HasColumnName("cost_amount_expected_acy").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountNonInvtblAcy).HasColumnName("cost_amount_non_invtbl_acy").HasPrecision(18, 5);
        builder.Property(x => x.ExpectedCostPostedToGL).HasColumnName("expected_cost_posted_to_g_l").HasPrecision(18, 5);
        builder.Property(x => x.ExpCostPostedToGLAcy).HasColumnName("exp_cost_posted_to_g_l_acy").HasPrecision(18, 5);
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.JobLedgerEntryNo).HasColumnName("job_ledger_entry_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.Adjustment).HasColumnName("adjustment");
        builder.Property(x => x.AverageCostException).HasColumnName("average_cost_exception");
        builder.Property(x => x.CapacityLedgerEntryNo).HasColumnName("capacity_ledger_entry_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.ReturnReasonCode).HasColumnName("return_reason_code");
    }
}
