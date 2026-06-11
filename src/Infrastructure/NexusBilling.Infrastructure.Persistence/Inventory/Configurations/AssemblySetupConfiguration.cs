using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class AssemblySetupConfiguration : IEntityTypeConfiguration<AssemblySetup>
{
    public void Configure(EntityTypeBuilder<AssemblySetup> builder)
    {
        builder.ToTable("assembly_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.StockoutWarning).HasColumnName("stockout_warning");
        builder.Property(x => x.AssemblyOrderNos).HasColumnName("assembly_order_nos");
        builder.Property(x => x.AssemblyQuoteNos).HasColumnName("assembly_quote_nos");
        builder.Property(x => x.BlanketAssemblyOrderNos).HasColumnName("blanket_assembly_order_nos");
        builder.Property(x => x.PostedAssemblyOrderNos).HasColumnName("posted_assembly_order_nos");
        builder.Property(x => x.CopyComponentDimensionsFrom).HasColumnName("copy_component_dimensions_from");
        builder.Property(x => x.DefaultLocationForOrders).HasColumnName("default_location_for_orders");
        builder.Property(x => x.CopyCommentsWhenPosting).HasColumnName("copy_comments_when_posting");
        builder.Property(x => x.CreateMovementsAutomatically).HasColumnName("create_movements_automatically");
    }
}
