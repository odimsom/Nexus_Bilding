using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigPackageDataConfiguration : IEntityTypeConfiguration<ConfigPackageData>
{
    public void Configure(EntityTypeBuilder<ConfigPackageData> builder)
    {
        builder.ToTable("config_package_data", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.Value).HasColumnName("value");
        builder.Property(x => x.Invalid).HasColumnName("invalid");
        builder.Property(x => x.BlobValue).HasColumnName("blob_value");
    }
}
