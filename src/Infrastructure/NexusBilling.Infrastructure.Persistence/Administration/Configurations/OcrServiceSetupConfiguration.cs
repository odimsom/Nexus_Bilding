using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OcrServiceSetupConfiguration : IEntityTypeConfiguration<OcrServiceSetup>
{
    public void Configure(EntityTypeBuilder<OcrServiceSetup> builder)
    {
        builder.ToTable("ocr_service_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.PasswordKey).HasColumnName("password_key");
        builder.Property(x => x.SignUpUrl).HasColumnName("sign_up_url");
        builder.Property(x => x.ServiceUrl).HasColumnName("service_url");
        builder.Property(x => x.SignInUrl).HasColumnName("sign_in_url");
        builder.Property(x => x.AuthorizationKey).HasColumnName("authorization_key");
        builder.Property(x => x.CustomerName).HasColumnName("customer_name");
        builder.Property(x => x.CustomerId).HasColumnName("customer_id");
        builder.Property(x => x.CustomerStatus).HasColumnName("customer_status");
        builder.Property(x => x.OrganizationId).HasColumnName("organization_id");
        builder.Property(x => x.DefaultOcrDocTemplate).HasColumnName("default_ocr_doc_template");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.MasterDataSyncEnabled).HasColumnName("master_data_sync_enabled");
        builder.Property(x => x.MasterDataLastSync).HasColumnName("master_data_last_sync");
    }
}
