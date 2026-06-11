using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourceGroupConfiguration : IEntityTypeConfiguration<ResourceGroup>
{
    public void Configure(EntityTypeBuilder<ResourceGroup> builder)
    {
        builder.ToTable("resource_group", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
    }
}
