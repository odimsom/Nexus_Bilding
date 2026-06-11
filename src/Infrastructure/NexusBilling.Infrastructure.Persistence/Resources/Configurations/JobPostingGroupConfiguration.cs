using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobPostingGroupConfiguration : IEntityTypeConfiguration<JobPostingGroup>
{
    public void Configure(EntityTypeBuilder<JobPostingGroup> builder)
    {
        builder.ToTable("job_posting_group", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.WipCostsAccount).HasColumnName("wip_costs_account");
        builder.Property(x => x.WipAccruedCostsAccount).HasColumnName("wip_accrued_costs_account");
        builder.Property(x => x.JobCostsAppliedAccount).HasColumnName("job_costs_applied_account");
        builder.Property(x => x.JobCostsAdjustmentAccount).HasColumnName("job_costs_adjustment_account");
        builder.Property(x => x.GLExpenseAccContract).HasColumnName("g_l_expense_acc_contract");
        builder.Property(x => x.JobSalesAdjustmentAccount).HasColumnName("job_sales_adjustment_account");
        builder.Property(x => x.WipAccruedSalesAccount).HasColumnName("wip_accrued_sales_account");
        builder.Property(x => x.WipInvoicedSalesAccount).HasColumnName("wip_invoiced_sales_account");
        builder.Property(x => x.JobSalesAppliedAccount).HasColumnName("job_sales_applied_account");
        builder.Property(x => x.RecognizedCostsAccount).HasColumnName("recognized_costs_account");
        builder.Property(x => x.RecognizedSalesAccount).HasColumnName("recognized_sales_account");
        builder.Property(x => x.ItemCostsAppliedAccount).HasColumnName("item_costs_applied_account");
        builder.Property(x => x.ResourceCostsAppliedAccount).HasColumnName("resource_costs_applied_account");
        builder.Property(x => x.GLCostsAppliedAccount).HasColumnName("g_l_costs_applied_account");
    }
}
