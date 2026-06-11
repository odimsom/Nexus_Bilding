using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ExchangeServiceSetupConfiguration : IEntityTypeConfiguration<ExchangeServiceSetup>
{
    public void Configure(EntityTypeBuilder<ExchangeServiceSetup> builder)
    {
        builder.ToTable("exchange_service_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AzureAdAppId).HasColumnName("azure_ad_app_id");
        builder.Property(x => x.AzureAdAppCertThumbprint).HasColumnName("azure_ad_app_cert_thumbprint");
        builder.Property(x => x.AzureAdAuthEndpoint).HasColumnName("azure_ad_auth_endpoint");
        builder.Property(x => x.ExchangeServiceEndpoint).HasColumnName("exchange_service_endpoint");
        builder.Property(x => x.ExchangeResourceUri).HasColumnName("exchange_resource_uri");
    }
}
