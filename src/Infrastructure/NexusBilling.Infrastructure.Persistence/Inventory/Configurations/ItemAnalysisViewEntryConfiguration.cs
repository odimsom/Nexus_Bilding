using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemAnalysisViewEntryConfiguration : IEntityTypeConfiguration<ItemAnalysisViewEntry>
{
    public void Configure(EntityTypeBuilder<ItemAnalysisViewEntry> builder)
    {
        builder.ToTable("item_analysis_view_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.AnalysisViewCode).HasColumnName("analysis_view_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Dimension1ValueCode).HasColumnName("dimension_1_value_code");
        builder.Property(x => x.Dimension2ValueCode).HasColumnName("dimension_2_value_code");
        builder.Property(x => x.Dimension3ValueCode).HasColumnName("dimension_3_value_code");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemLedgerEntryType).HasColumnName("item_ledger_entry_type");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.InvoicedQuantity).HasColumnName("invoiced_quantity").HasPrecision(18, 5);
        builder.Property(x => x.SalesAmountActual).HasColumnName("sales_amount_actual").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountActual).HasColumnName("cost_amount_actual").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountNonInvtbl).HasColumnName("cost_amount_non_invtbl").HasPrecision(18, 5);
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.SalesAmountExpected).HasColumnName("sales_amount_expected").HasPrecision(18, 5);
        builder.Property(x => x.CostAmountExpected).HasColumnName("cost_amount_expected").HasPrecision(18, 5);
    }
}
