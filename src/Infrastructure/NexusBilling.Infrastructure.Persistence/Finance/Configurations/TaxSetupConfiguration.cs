using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class TaxSetupConfiguration : IEntityTypeConfiguration<TaxSetup>
{
    public void Configure(EntityTypeBuilder<TaxSetup> builder)
    {
        builder.ToTable("tax_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AutoCreateTaxDetails).HasColumnName("auto_create_tax_details");
        builder.Property(x => x.NonTaxableTaxGroupCode).HasColumnName("non_taxable_tax_group_code");
        builder.Property(x => x.TaxAccountSales).HasColumnName("tax_account_sales");
        builder.Property(x => x.TaxAccountPurchases).HasColumnName("tax_account_purchases");
        builder.Property(x => x.UnrealTaxAccSales).HasColumnName("unreal_tax_acc_sales");
        builder.Property(x => x.UnrealTaxAccPurchases).HasColumnName("unreal_tax_acc_purchases");
        builder.Property(x => x.ReverseChargePurchases).HasColumnName("reverse_charge_purchases");
        builder.Property(x => x.UnrealRevChargePurch).HasColumnName("unreal_rev_charge_purch");
    }
}
