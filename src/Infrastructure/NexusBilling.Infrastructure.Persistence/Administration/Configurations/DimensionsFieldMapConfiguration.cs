using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionsFieldMapConfiguration : IEntityTypeConfiguration<DimensionsFieldMap>
{
    public void Configure(EntityTypeBuilder<DimensionsFieldMap> builder)
    {
        builder.ToTable("dimensions_field_map", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableNo).HasColumnName("table_no");
        builder.Property(x => x.GlobalDim1FieldNo).HasColumnName("global_dim_1_field_no");
        builder.Property(x => x.GlobalDim2FieldNo).HasColumnName("global_dim_2_field_no");
        builder.Property(x => x.IdFieldNo).HasColumnName("id_field_no");
    }
}
