using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GeneralPostingSetupConfiguration : IEntityTypeConfiguration<GeneralPostingSetup>
{
    public void Configure(EntityTypeBuilder<GeneralPostingSetup> builder)
    {
        builder.ToTable("general_posting_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.SalesAccount).HasColumnName("sales_account");
        builder.Property(x => x.SalesLineDiscAccount).HasColumnName("sales_line_disc_account");
        builder.Property(x => x.SalesInvDiscAccount).HasColumnName("sales_inv_disc_account");
        builder.Property(x => x.SalesPmtDiscDebitAcc).HasColumnName("sales_pmt_disc_debit_acc");
        builder.Property(x => x.PurchAccount).HasColumnName("purch_account");
        builder.Property(x => x.PurchLineDiscAccount).HasColumnName("purch_line_disc_account");
        builder.Property(x => x.PurchInvDiscAccount).HasColumnName("purch_inv_disc_account");
        builder.Property(x => x.PurchPmtDiscCreditAcc).HasColumnName("purch_pmt_disc_credit_acc");
        builder.Property(x => x.CogsAccount).HasColumnName("cogs_account");
        builder.Property(x => x.InventoryAdjmtAccount).HasColumnName("inventory_adjmt_account");
        builder.Property(x => x.SalesCreditMemoAccount).HasColumnName("sales_credit_memo_account");
        builder.Property(x => x.PurchCreditMemoAccount).HasColumnName("purch_credit_memo_account");
        builder.Property(x => x.SalesPmtDiscCreditAcc).HasColumnName("sales_pmt_disc_credit_acc");
        builder.Property(x => x.PurchPmtDiscDebitAcc).HasColumnName("purch_pmt_disc_debit_acc");
        builder.Property(x => x.SalesPmtTolDebitAcc).HasColumnName("sales_pmt_tol_debit_acc");
        builder.Property(x => x.SalesPmtTolCreditAcc).HasColumnName("sales_pmt_tol_credit_acc");
        builder.Property(x => x.PurchPmtTolDebitAcc).HasColumnName("purch_pmt_tol_debit_acc");
        builder.Property(x => x.PurchPmtTolCreditAcc).HasColumnName("purch_pmt_tol_credit_acc");
        builder.Property(x => x.SalesPrepaymentsAccount).HasColumnName("sales_prepayments_account");
        builder.Property(x => x.PurchPrepaymentsAccount).HasColumnName("purch_prepayments_account");
        builder.Property(x => x.PurchFaDiscAccount).HasColumnName("purch_fa_disc_account");
        builder.Property(x => x.InvtAccrualAccInterim).HasColumnName("invt_accrual_acc_interim");
        builder.Property(x => x.CogsAccountInterim).HasColumnName("cogs_account_interim");
        builder.Property(x => x.DirectCostAppliedAccount).HasColumnName("direct_cost_applied_account");
        builder.Property(x => x.OverheadAppliedAccount).HasColumnName("overhead_applied_account");
        builder.Property(x => x.PurchaseVarianceAccount).HasColumnName("purchase_variance_account");
    }
}
