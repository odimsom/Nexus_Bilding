using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OfficeAddInConfiguration : IEntityTypeConfiguration<OfficeAddIn>
{
    public void Configure(EntityTypeBuilder<OfficeAddIn> builder)
    {
        builder.ToTable("office_add_in", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ApplicationId).HasColumnName("application_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Version).HasColumnName("version");
        builder.Property(x => x.ManifestCodeunit).HasColumnName("manifest_codeunit");
        builder.Property(x => x.DeploymentDate).HasColumnName("deployment_date");
        builder.Property(x => x.DefaultManifest).HasColumnName("default_manifest");
        builder.Property(x => x.Manifest).HasColumnName("manifest");
        builder.Property(x => x.Breaking).HasColumnName("breaking");
    }
}
