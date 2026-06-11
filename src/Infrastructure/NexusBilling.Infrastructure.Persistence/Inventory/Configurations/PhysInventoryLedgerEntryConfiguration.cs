using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PhysInventoryLedgerEntryConfiguration : IEntityTypeConfiguration<PhysInventoryLedgerEntry>
{
    public void Configure(EntityTypeBuilder<PhysInventoryLedgerEntry> builder)
    {
        builder.ToTable("phys_inventory_ledger_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitAmount).HasColumnName("unit_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.QtyCalculated).HasColumnName("qty_calculated").HasPrecision(18, 5);
        builder.Property(x => x.QtyPhysInventory).HasColumnName("qty_phys_inventory").HasPrecision(18, 5);
        builder.Property(x => x.LastItemLedgerEntryNo).HasColumnName("last_item_ledger_entry_no");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.PhysInvtCountingPeriodCode).HasColumnName("phys_invt_counting_period_code");
        builder.Property(x => x.PhysInvtCountingPeriodType).HasColumnName("phys_invt_counting_period_type");
    }
}
