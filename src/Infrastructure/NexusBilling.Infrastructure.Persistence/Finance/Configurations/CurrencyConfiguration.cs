using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currency", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.LastDateAdjusted).HasColumnName("last_date_adjusted");
        builder.Property(x => x.UnrealizedGainsAcc).HasColumnName("unrealized_gains_acc");
        builder.Property(x => x.RealizedGainsAcc).HasColumnName("realized_gains_acc");
        builder.Property(x => x.UnrealizedLossesAcc).HasColumnName("unrealized_losses_acc");
        builder.Property(x => x.RealizedLossesAcc).HasColumnName("realized_losses_acc");
        builder.Property(x => x.InvoiceRoundingPrecision).HasColumnName("invoice_rounding_precision").HasPrecision(18, 5);
        builder.Property(x => x.InvoiceRoundingType).HasColumnName("invoice_rounding_type");
        builder.Property(x => x.AmountRoundingPrecision).HasColumnName("amount_rounding_precision").HasPrecision(18, 5);
        builder.Property(x => x.UnitAmountRoundingPrecision).HasColumnName("unit_amount_rounding_precision").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AmountDecimalPlaces).HasColumnName("amount_decimal_places");
        builder.Property(x => x.UnitAmountDecimalPlaces).HasColumnName("unit_amount_decimal_places");
        builder.Property(x => x.RealizedGLGainsAccount).HasColumnName("realized_g_l_gains_account");
        builder.Property(x => x.RealizedGLLossesAccount).HasColumnName("realized_g_l_losses_account");
        builder.Property(x => x.ApplnRoundingPrecision).HasColumnName("appln_rounding_precision").HasPrecision(18, 5);
        builder.Property(x => x.EmuCurrency).HasColumnName("emu_currency");
        builder.Property(x => x.CurrencyFactor).HasColumnName("currency_factor").HasPrecision(18, 5);
        builder.Property(x => x.ResidualGainsAccount).HasColumnName("residual_gains_account");
        builder.Property(x => x.ResidualLossesAccount).HasColumnName("residual_losses_account");
        builder.Property(x => x.ConvLcyRndgDebitAcc).HasColumnName("conv_lcy_rndg_debit_acc");
        builder.Property(x => x.ConvLcyRndgCreditAcc).HasColumnName("conv_lcy_rndg_credit_acc");
        builder.Property(x => x.MaxVatDifferenceAllowed).HasColumnName("max_vat_difference_allowed").HasPrecision(18, 5);
        builder.Property(x => x.VatRoundingType).HasColumnName("vat_rounding_type");
        builder.Property(x => x.PaymentTolerance).HasColumnName("payment_tolerance").HasPrecision(18, 5);
        builder.Property(x => x.MaxPaymentToleranceAmount).HasColumnName("max_payment_tolerance_amount").HasPrecision(18, 5);
        builder.Property(x => x.Symbol).HasColumnName("symbol");
    }
}
