using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class VendorPostingGroupConfiguration : IEntityTypeConfiguration<VendorPostingGroup>
{
    public void Configure(EntityTypeBuilder<VendorPostingGroup> builder)
    {
        builder.ToTable("vendor_posting_group", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.PayablesAccount).HasColumnName("payables_account");
        builder.Property(x => x.ServiceChargeAcc).HasColumnName("service_charge_acc");
        builder.Property(x => x.PaymentDiscDebitAcc).HasColumnName("payment_disc_debit_acc");
        builder.Property(x => x.InvoiceRoundingAccount).HasColumnName("invoice_rounding_account");
        builder.Property(x => x.DebitCurrApplnRndgAcc).HasColumnName("debit_curr_appln_rndg_acc");
        builder.Property(x => x.CreditCurrApplnRndgAcc).HasColumnName("credit_curr_appln_rndg_acc");
        builder.Property(x => x.DebitRoundingAccount).HasColumnName("debit_rounding_account");
        builder.Property(x => x.CreditRoundingAccount).HasColumnName("credit_rounding_account");
        builder.Property(x => x.PaymentDiscCreditAcc).HasColumnName("payment_disc_credit_acc");
        builder.Property(x => x.PaymentToleranceDebitAcc).HasColumnName("payment_tolerance_debit_acc");
        builder.Property(x => x.PaymentToleranceCreditAcc).HasColumnName("payment_tolerance_credit_acc");
    }
}
