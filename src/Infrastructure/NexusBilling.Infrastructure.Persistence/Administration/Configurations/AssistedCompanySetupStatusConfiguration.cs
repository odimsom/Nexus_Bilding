using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AssistedCompanySetupStatusConfiguration : IEntityTypeConfiguration<AssistedCompanySetupStatus>
{
    public void Configure(EntityTypeBuilder<AssistedCompanySetupStatus> builder)
    {
        builder.ToTable("assisted_company_setup_status", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.PackageImported).HasColumnName("package_imported");
        builder.Property(x => x.ImportFailed).HasColumnName("import_failed");
        builder.Property(x => x.CompanySetupSessionId).HasColumnName("company_setup_session_id");
    }
}
