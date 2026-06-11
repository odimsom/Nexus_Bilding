using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemChargeAssignmentPurchConfiguration : IEntityTypeConfiguration<ItemChargeAssignmentPurch>
{
    public void Configure(EntityTypeBuilder<ItemChargeAssignmentPurch> builder)
    {
        builder.ToTable("item_charge_assignment_purch", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.DocumentLineNo).HasColumnName("document_line_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ItemChargeNo).HasColumnName("item_charge_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.QtyToAssign).HasColumnName("qty_to_assign").HasPrecision(18, 5);
        builder.Property(x => x.QtyAssigned).HasColumnName("qty_assigned").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.AmountToAssign).HasColumnName("amount_to_assign").HasPrecision(18, 5);
        builder.Property(x => x.AppliesToDocType).HasColumnName("applies_to_doc_type");
        builder.Property(x => x.AppliesToDocNo).HasColumnName("applies_to_doc_no");
        builder.Property(x => x.AppliesToDocLineNo).HasColumnName("applies_to_doc_line_no");
        builder.Property(x => x.AppliesToDocLineAmount).HasColumnName("applies_to_doc_line_amount").HasPrecision(18, 5);
    }
}
