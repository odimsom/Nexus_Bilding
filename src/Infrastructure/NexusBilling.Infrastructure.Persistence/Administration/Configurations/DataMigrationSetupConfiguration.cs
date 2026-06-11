using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DataMigrationSetupConfiguration : IEntityTypeConfiguration<DataMigrationSetup>
{
    public void Configure(EntityTypeBuilder<DataMigrationSetup> builder)
    {
        builder.ToTable("data_migration_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.DefaultCustomerTemplate).HasColumnName("default_customer_template");
        builder.Property(x => x.DefaultVendorTemplate).HasColumnName("default_vendor_template");
        builder.Property(x => x.DefaultItemTemplate).HasColumnName("default_item_template");
        builder.Property(x => x.DefaultAccountTemplate).HasColumnName("default_account_template");
        builder.Property(x => x.DefaultPostingGroupTemplate).HasColumnName("default_posting_group_template");
        builder.Property(x => x.DefaultCustPostingTemplate).HasColumnName("default_cust_posting_template");
        builder.Property(x => x.DefaultVendPostingTemplate).HasColumnName("default_vend_posting_template");
    }
}
