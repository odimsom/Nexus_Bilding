using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustomerPostingGroupConfiguration : IEntityTypeConfiguration<CustomerPostingGroup>
{
    public void Configure(EntityTypeBuilder<CustomerPostingGroup> builder)
    {
        builder.ToTable("customer_posting_group", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.ReceivablesAccount).HasColumnName("receivables_account");
        builder.Property(x => x.ServiceChargeAcc).HasColumnName("service_charge_acc");
        builder.Property(x => x.PaymentDiscDebitAcc).HasColumnName("payment_disc_debit_acc");
        builder.Property(x => x.InvoiceRoundingAccount).HasColumnName("invoice_rounding_account");
        builder.Property(x => x.AdditionalFeeAccount).HasColumnName("additional_fee_account");
        builder.Property(x => x.InterestAccount).HasColumnName("interest_account");
        builder.Property(x => x.DebitCurrApplnRndgAcc).HasColumnName("debit_curr_appln_rndg_acc");
        builder.Property(x => x.CreditCurrApplnRndgAcc).HasColumnName("credit_curr_appln_rndg_acc");
        builder.Property(x => x.DebitRoundingAccount).HasColumnName("debit_rounding_account");
        builder.Property(x => x.CreditRoundingAccount).HasColumnName("credit_rounding_account");
        builder.Property(x => x.PaymentDiscCreditAcc).HasColumnName("payment_disc_credit_acc");
        builder.Property(x => x.PaymentToleranceDebitAcc).HasColumnName("payment_tolerance_debit_acc");
        builder.Property(x => x.PaymentToleranceCreditAcc).HasColumnName("payment_tolerance_credit_acc");
        builder.Property(x => x.AddFeePerLineAccount).HasColumnName("add_fee_per_line_account");
    }
}
