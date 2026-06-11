using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AzureAdMgtSetupConfiguration : IEntityTypeConfiguration<AzureAdMgtSetup>
{
    public void Configure(EntityTypeBuilder<AzureAdMgtSetup> builder)
    {
        builder.ToTable("azure_ad_mgt_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AuthFlowCodeunitId).HasColumnName("auth_flow_codeunit_id");
        builder.Property(x => x.AzureAdUserMgtCodeunitId).HasColumnName("azure_ad_user_mgt_codeunit_id");
    }
}
