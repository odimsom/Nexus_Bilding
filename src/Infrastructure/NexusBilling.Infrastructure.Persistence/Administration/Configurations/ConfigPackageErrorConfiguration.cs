using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigPackageErrorConfiguration : IEntityTypeConfiguration<ConfigPackageError>
{
    public void Configure(EntityTypeBuilder<ConfigPackageError> builder)
    {
        builder.ToTable("config_package_error", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.RecordNo).HasColumnName("record_no");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.ErrorText).HasColumnName("error_text");
        builder.Property(x => x.ErrorType).HasColumnName("error_type");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
    }
}
