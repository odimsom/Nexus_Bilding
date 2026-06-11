using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionsTemplateConfiguration : IEntityTypeConfiguration<DimensionsTemplate>
{
    public void Configure(EntityTypeBuilder<DimensionsTemplate> builder)
    {
        builder.ToTable("dimensions_template", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueCode).HasColumnName("dimension_value_code");
        builder.Property(x => x.ValuePosting).HasColumnName("value_posting");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.MasterRecordTemplateCode).HasColumnName("master_record_template_code");
    }
}
