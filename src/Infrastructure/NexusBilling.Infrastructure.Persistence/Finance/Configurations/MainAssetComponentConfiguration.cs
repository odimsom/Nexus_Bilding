using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class MainAssetComponentConfiguration : IEntityTypeConfiguration<MainAssetComponent>
{
    public void Configure(EntityTypeBuilder<MainAssetComponent> builder)
    {
        builder.ToTable("main_asset_component", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.MainAssetNo).HasColumnName("main_asset_no");
        builder.Property(x => x.FaNo).HasColumnName("fa_no");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
