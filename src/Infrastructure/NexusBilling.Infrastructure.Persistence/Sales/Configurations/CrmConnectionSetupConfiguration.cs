using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CrmConnectionSetupConfiguration : IEntityTypeConfiguration<CrmConnectionSetup>
{
    public void Configure(EntityTypeBuilder<CrmConnectionSetup> builder)
    {
        builder.ToTable("crm_connection_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.ServerAddress).HasColumnName("server_address");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.UserPasswordKey).HasColumnName("user_password_key");
        builder.Property(x => x.LastUpdateInvoiceEntryNo).HasColumnName("last_update_invoice_entry_no");
        builder.Property(x => x.IsEnabled).HasColumnName("is_enabled");
        builder.Property(x => x.IsUserMappingRequired).HasColumnName("is_user_mapping_required");
        builder.Property(x => x.IsUserMappedToCrmUser).HasColumnName("is_user_mapped_to_crm_user");
        builder.Property(x => x.CrmVersion).HasColumnName("crm_version");
        builder.Property(x => x.IsSOrderIntegrationEnabled).HasColumnName("is_s_order_integration_enabled");
        builder.Property(x => x.IsCrmSolutionInstalled).HasColumnName("is_crm_solution_installed");
        builder.Property(x => x.IsEnabledForUser).HasColumnName("is_enabled_for_user");
        builder.Property(x => x.DynamicsNavUrl).HasColumnName("dynamics_nav_url");
        builder.Property(x => x.DynamicsNavOdataUrl).HasColumnName("dynamics_nav_odata_url");
        builder.Property(x => x.DynamicsNavOdataUsername).HasColumnName("dynamics_nav_odata_username");
        builder.Property(x => x.DynamicsNavOdataAccesskey).HasColumnName("dynamics_nav_odata_accesskey");
        builder.Property(x => x.DefaultCrmPriceListId).HasColumnName("default_crm_price_list_id");
        builder.Property(x => x.AuthenticationType).HasColumnName("authentication_type");
        builder.Property(x => x.ConnectionString).HasColumnName("connection_string");
        builder.Property(x => x.Domain).HasColumnName("domain");
        builder.Property(x => x.ServerConnectionString).HasColumnName("server_connection_string");
    }
}
