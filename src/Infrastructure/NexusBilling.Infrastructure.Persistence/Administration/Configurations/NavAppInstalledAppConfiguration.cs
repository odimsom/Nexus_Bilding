using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NavAppInstalledAppConfiguration : IEntityTypeConfiguration<NavAppInstalledApp>
{
    public void Configure(EntityTypeBuilder<NavAppInstalledApp> builder)
    {
        builder.ToTable("nav_app_installed_app", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AppId).HasColumnName("app_id");
        builder.Property(x => x.PackageId).HasColumnName("package_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Publisher).HasColumnName("publisher");
        builder.Property(x => x.VersionMajor).HasColumnName("version_major");
        builder.Property(x => x.VersionMinor).HasColumnName("version_minor");
        builder.Property(x => x.VersionBuild).HasColumnName("version_build");
        builder.Property(x => x.VersionRevision).HasColumnName("version_revision");
        builder.Property(x => x.CompatibilityMajor).HasColumnName("compatibility_major");
        builder.Property(x => x.CompatibilityMinor).HasColumnName("compatibility_minor");
        builder.Property(x => x.CompatibilityBuild).HasColumnName("compatibility_build");
        builder.Property(x => x.CompatibilityRevision).HasColumnName("compatibility_revision");
    }
}
