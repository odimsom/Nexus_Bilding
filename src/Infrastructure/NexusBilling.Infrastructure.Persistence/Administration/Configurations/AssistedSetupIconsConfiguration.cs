using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AssistedSetupIconsConfiguration : IEntityTypeConfiguration<AssistedSetupIcons>
{
    public void Configure(EntityTypeBuilder<AssistedSetupIcons> builder)
    {
        builder.ToTable("assisted_setup_icons", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Image).HasColumnName("image");
    }
}
