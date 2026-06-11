using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BinCreationWkshNameConfiguration : IEntityTypeConfiguration<BinCreationWkshName>
{
    public void Configure(EntityTypeBuilder<BinCreationWkshName> builder)
    {
        builder.ToTable("bin_creation_wksh_name", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.WorksheetTemplateName).HasColumnName("worksheet_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
    }
}
