using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaPostingGroupConfiguration : IEntityTypeConfiguration<FaPostingGroup>
{
    public void Configure(EntityTypeBuilder<FaPostingGroup> builder)
    {
        builder.ToTable("fa_posting_group", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.AcquisitionCostAccount).HasColumnName("acquisition_cost_account");
        builder.Property(x => x.AccumDepreciationAccount).HasColumnName("accum_depreciation_account");
        builder.Property(x => x.WriteDownAccount).HasColumnName("write_down_account");
        builder.Property(x => x.AppreciationAccount).HasColumnName("appreciation_account");
        builder.Property(x => x.Custom1Account).HasColumnName("custom_1_account");
        builder.Property(x => x.Custom2Account).HasColumnName("custom_2_account");
        builder.Property(x => x.AcqCostAccOnDisposal).HasColumnName("acq_cost_acc_on_disposal");
        builder.Property(x => x.AccumDeprAccOnDisposal).HasColumnName("accum_depr_acc_on_disposal");
        builder.Property(x => x.WriteDownAccOnDisposal).HasColumnName("write_down_acc_on_disposal");
        builder.Property(x => x.AppreciationAccOnDisposal).HasColumnName("appreciation_acc_on_disposal");
        builder.Property(x => x.Custom1AccountOnDisposal).HasColumnName("custom_1_account_on_disposal");
        builder.Property(x => x.Custom2AccountOnDisposal).HasColumnName("custom_2_account_on_disposal");
        builder.Property(x => x.GainsAccOnDisposal).HasColumnName("gains_acc_on_disposal");
        builder.Property(x => x.LossesAccOnDisposal).HasColumnName("losses_acc_on_disposal");
        builder.Property(x => x.BookValAccOnDispGain).HasColumnName("book_val_acc_on_disp_gain");
        builder.Property(x => x.SalesAccOnDispGain).HasColumnName("sales_acc_on_disp_gain");
        builder.Property(x => x.WriteDownBalAccOnDisp).HasColumnName("write_down_bal_acc_on_disp");
        builder.Property(x => x.ApprecBalAccOnDisp).HasColumnName("apprec_bal_acc_on_disp");
        builder.Property(x => x.Custom1BalAccOnDisposal).HasColumnName("custom_1_bal_acc_on_disposal");
        builder.Property(x => x.Custom2BalAccOnDisposal).HasColumnName("custom_2_bal_acc_on_disposal");
        builder.Property(x => x.MaintenanceExpenseAccount).HasColumnName("maintenance_expense_account");
        builder.Property(x => x.MaintenanceBalAcc).HasColumnName("maintenance_bal_acc");
        builder.Property(x => x.AcquisitionCostBalAcc).HasColumnName("acquisition_cost_bal_acc");
        builder.Property(x => x.DepreciationExpenseAcc).HasColumnName("depreciation_expense_acc");
        builder.Property(x => x.WriteDownExpenseAcc).HasColumnName("write_down_expense_acc");
        builder.Property(x => x.AppreciationBalAccount).HasColumnName("appreciation_bal_account");
        builder.Property(x => x.Custom1ExpenseAcc).HasColumnName("custom_1_expense_acc");
        builder.Property(x => x.Custom2ExpenseAcc).HasColumnName("custom_2_expense_acc");
        builder.Property(x => x.SalesBalAcc).HasColumnName("sales_bal_acc");
        builder.Property(x => x.SalesAccOnDispLoss).HasColumnName("sales_acc_on_disp_loss");
        builder.Property(x => x.BookValAccOnDispLoss).HasColumnName("book_val_acc_on_disp_loss");
    }
}
