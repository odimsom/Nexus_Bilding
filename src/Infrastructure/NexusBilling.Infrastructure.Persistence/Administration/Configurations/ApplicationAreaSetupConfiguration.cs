using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ApplicationAreaSetupConfiguration : IEntityTypeConfiguration<ApplicationAreaSetup>
{
    public void Configure(EntityTypeBuilder<ApplicationAreaSetup> builder)
    {
        builder.ToTable("application_area_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.ProfileId).HasColumnName("profile_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Basic).HasColumnName("basic");
        builder.Property(x => x.Suite).HasColumnName("suite");
        builder.Property(x => x.RelationshipMgmt).HasColumnName("relationship_mgmt");
        builder.Property(x => x.Jobs).HasColumnName("jobs");
        builder.Property(x => x.FixedAssets).HasColumnName("fixed_assets");
    }
}
