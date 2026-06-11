using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class NonstockItemSetupConfiguration : IEntityTypeConfiguration<NonstockItemSetup>
{
    public void Configure(EntityTypeBuilder<NonstockItemSetup> builder)
    {
        builder.ToTable("nonstock_item_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.NoFormat).HasColumnName("no_format");
        builder.Property(x => x.NoFormatSeparator).HasColumnName("no_format_separator");
    }
}
