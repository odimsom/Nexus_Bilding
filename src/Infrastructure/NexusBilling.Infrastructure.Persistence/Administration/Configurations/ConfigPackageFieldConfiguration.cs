using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigPackageFieldConfiguration : IEntityTypeConfiguration<ConfigPackageField>
{
    public void Configure(EntityTypeBuilder<ConfigPackageField> builder)
    {
        builder.ToTable("config_package_field", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.FieldName).HasColumnName("field_name");
        builder.Property(x => x.FieldCaption).HasColumnName("field_caption");
        builder.Property(x => x.ValidateField).HasColumnName("validate_field");
        builder.Property(x => x.IncludeField).HasColumnName("include_field");
        builder.Property(x => x.LocalizeField).HasColumnName("localize_field");
        builder.Property(x => x.RelationTableId).HasColumnName("relation_table_id");
        builder.Property(x => x.Dimension).HasColumnName("dimension");
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.ProcessingOrder).HasColumnName("processing_order");
        builder.Property(x => x.CreateMissingCodes).HasColumnName("create_missing_codes");
    }
}
