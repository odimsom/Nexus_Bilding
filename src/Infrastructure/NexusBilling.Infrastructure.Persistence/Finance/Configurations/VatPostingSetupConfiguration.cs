using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class VatPostingSetupConfiguration : IEntityTypeConfiguration<VatPostingSetup>
{
    public void Configure(EntityTypeBuilder<VatPostingSetup> builder)
    {
        builder.ToTable("vat_posting_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.VatCalculationType).HasColumnName("vat_calculation_type");
        builder.Property(x => x.Vat).HasColumnName("vat").HasPrecision(18, 5);
        builder.Property(x => x.UnrealizedVatType).HasColumnName("unrealized_vat_type");
        builder.Property(x => x.AdjustForPaymentDiscount).HasColumnName("adjust_for_payment_discount");
        builder.Property(x => x.SalesVatAccount).HasColumnName("sales_vat_account");
        builder.Property(x => x.SalesVatUnrealAccount).HasColumnName("sales_vat_unreal_account");
        builder.Property(x => x.PurchaseVatAccount).HasColumnName("purchase_vat_account");
        builder.Property(x => x.PurchVatUnrealAccount).HasColumnName("purch_vat_unreal_account");
        builder.Property(x => x.ReverseChrgVatAcc).HasColumnName("reverse_chrg_vat_acc");
        builder.Property(x => x.ReverseChrgVatUnrealAcc).HasColumnName("reverse_chrg_vat_unreal_acc");
        builder.Property(x => x.VatIdentifier).HasColumnName("vat_identifier");
        builder.Property(x => x.EuService).HasColumnName("eu_service");
        builder.Property(x => x.VatClauseCode).HasColumnName("vat_clause_code");
        builder.Property(x => x.CertificateOfSupplyRequired).HasColumnName("certificate_of_supply_required");
        builder.Property(x => x.TaxCategory).HasColumnName("tax_category");
    }
}
