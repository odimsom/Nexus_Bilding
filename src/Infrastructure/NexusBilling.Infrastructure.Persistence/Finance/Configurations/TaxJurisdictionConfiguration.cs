using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class TaxJurisdictionConfiguration : IEntityTypeConfiguration<TaxJurisdiction>
{
    public void Configure(EntityTypeBuilder<TaxJurisdiction> builder)
    {
        builder.ToTable("tax_jurisdiction", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TaxAccountSales).HasColumnName("tax_account_sales");
        builder.Property(x => x.TaxAccountPurchases).HasColumnName("tax_account_purchases");
        builder.Property(x => x.ReportToJurisdiction).HasColumnName("report_to_jurisdiction");
        builder.Property(x => x.UnrealTaxAccSales).HasColumnName("unreal_tax_acc_sales");
        builder.Property(x => x.UnrealTaxAccPurchases).HasColumnName("unreal_tax_acc_purchases");
        builder.Property(x => x.ReverseChargePurchases).HasColumnName("reverse_charge_purchases");
        builder.Property(x => x.UnrealRevChargePurch).HasColumnName("unreal_rev_charge_purch");
        builder.Property(x => x.UnrealizedVatType).HasColumnName("unrealized_vat_type");
        builder.Property(x => x.CalculateTaxOnTax).HasColumnName("calculate_tax_on_tax");
        builder.Property(x => x.AdjustForPaymentDiscount).HasColumnName("adjust_for_payment_discount");
    }
}
