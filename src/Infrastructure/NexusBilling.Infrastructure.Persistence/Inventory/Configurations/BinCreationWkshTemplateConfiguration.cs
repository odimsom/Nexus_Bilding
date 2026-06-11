using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BinCreationWkshTemplateConfiguration : IEntityTypeConfiguration<BinCreationWkshTemplate>
{
    public void Configure(EntityTypeBuilder<BinCreationWkshTemplate> builder)
    {
        builder.ToTable("bin_creation_wksh_template", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.Type).HasColumnName("type");
    }
}
