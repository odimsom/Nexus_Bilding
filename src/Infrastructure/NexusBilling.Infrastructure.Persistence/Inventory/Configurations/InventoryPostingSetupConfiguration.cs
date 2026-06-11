using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class InventoryPostingSetupConfiguration : IEntityTypeConfiguration<InventoryPostingSetup>
{
    public void Configure(EntityTypeBuilder<InventoryPostingSetup> builder)
    {
        builder.ToTable("inventory_posting_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.InvtPostingGroupCode).HasColumnName("invt_posting_group_code");
        builder.Property(x => x.InventoryAccount).HasColumnName("inventory_account");
        builder.Property(x => x.InventoryAccountInterim).HasColumnName("inventory_account_interim");
        builder.Property(x => x.WipAccount).HasColumnName("wip_account");
        builder.Property(x => x.MaterialVarianceAccount).HasColumnName("material_variance_account");
        builder.Property(x => x.CapacityVarianceAccount).HasColumnName("capacity_variance_account");
        builder.Property(x => x.MfgOverheadVarianceAccount).HasColumnName("mfg_overhead_variance_account");
        builder.Property(x => x.CapOverheadVarianceAccount).HasColumnName("cap_overhead_variance_account");
        builder.Property(x => x.SubcontractedVarianceAccount).HasColumnName("subcontracted_variance_account");
    }
}
