using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AssistedSetupConfiguration : IEntityTypeConfiguration<AssistedSetup>
{
    public void Configure(EntityTypeBuilder<AssistedSetup> builder)
    {
        builder.ToTable("assisted_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Order).HasColumnName("order");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Visible).HasColumnName("visible");
        builder.Property(x => x.Parent).HasColumnName("parent");
        builder.Property(x => x.VideoUrl).HasColumnName("video_url");
        builder.Property(x => x.Icon).HasColumnName("icon");
        builder.Property(x => x.ItemType).HasColumnName("item_type");
        builder.Property(x => x.Featured).HasColumnName("featured");
        builder.Property(x => x.HelpUrl).HasColumnName("help_url");
        builder.Property(x => x.AssistedSetupPageId).HasColumnName("assisted_setup_page_id");
        builder.Property(x => x.TourId).HasColumnName("tour_id");
        builder.Property(x => x.VideoStatus).HasColumnName("video_status");
        builder.Property(x => x.HelpStatus).HasColumnName("help_status");
        builder.Property(x => x.TourStatus).HasColumnName("tour_status");
    }
}
