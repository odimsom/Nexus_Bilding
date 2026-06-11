using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigPackageConfiguration : IEntityTypeConfiguration<ConfigPackage>
{
    public void Configure(EntityTypeBuilder<ConfigPackage> builder)
    {
        builder.ToTable("config_package", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.PackageName).HasColumnName("package_name");
        builder.Property(x => x.LanguageId).HasColumnName("language_id");
        builder.Property(x => x.ProductVersion).HasColumnName("product_version");
        builder.Property(x => x.ExcludeConfigTables).HasColumnName("exclude_config_tables");
        builder.Property(x => x.ProcessingOrder).HasColumnName("processing_order");
    }
}
