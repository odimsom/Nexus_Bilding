using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AzureAdAppSetupConfiguration : IEntityTypeConfiguration<AzureAdAppSetup>
{
    public void Configure(EntityTypeBuilder<AzureAdAppSetup> builder)
    {
        builder.ToTable("azure_ad_app_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AppId).HasColumnName("app_id");
        builder.Property(x => x.SecretKey).HasColumnName("secret_key");
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.RedirectUrl).HasColumnName("redirect_url");
    }
}
