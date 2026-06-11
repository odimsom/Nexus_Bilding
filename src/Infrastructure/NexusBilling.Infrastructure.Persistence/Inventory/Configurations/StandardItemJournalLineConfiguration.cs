using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class StandardItemJournalLineConfiguration : IEntityTypeConfiguration<StandardItemJournalLine>
{
    public void Configure(EntityTypeBuilder<StandardItemJournalLine> builder)
    {
        builder.ToTable("standard_item_journal_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitAmount).HasColumnName("unit_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.StandardJournalCode).HasColumnName("standard_journal_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.TransactionType).HasColumnName("transaction_type");
        builder.Property(x => x.TransportMethod).HasColumnName("transport_method");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.QtyCalculated).HasColumnName("qty_calculated").HasPrecision(18, 5);
        builder.Property(x => x.QtyPhysInventory).HasColumnName("qty_phys_inventory").HasPrecision(18, 5);
        builder.Property(x => x.PhysInventory).HasColumnName("phys_inventory");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.EntryExitPoint).HasColumnName("entry_exit_point");
        builder.Property(x => x.Area).HasColumnName("area");
        builder.Property(x => x.TransactionSpecification).HasColumnName("transaction_specification");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.OriginallyOrderedNo).HasColumnName("originally_ordered_no");
        builder.Property(x => x.OriginallyOrderedVarCode).HasColumnName("originally_ordered_var_code");
        builder.Property(x => x.ItemCategoryCode).HasColumnName("item_category_code");
        builder.Property(x => x.Nonstock).HasColumnName("nonstock");
        builder.Property(x => x.PurchasingCode).HasColumnName("purchasing_code");
        builder.Property(x => x.ProductGroupCode).HasColumnName("product_group_code");
        builder.Property(x => x.ValueEntryType).HasColumnName("value_entry_type");
        builder.Property(x => x.ItemChargeNo).HasColumnName("item_charge_no");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.WorkCenterNo).HasColumnName("work_center_no");
        builder.Property(x => x.ReturnReasonCode).HasColumnName("return_reason_code");
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
    }
}
