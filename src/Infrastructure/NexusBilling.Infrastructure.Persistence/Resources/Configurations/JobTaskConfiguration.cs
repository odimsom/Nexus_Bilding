using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobTaskConfiguration : IEntityTypeConfiguration<JobTask>
{
    public void Configure(EntityTypeBuilder<JobTask> builder)
    {
        builder.ToTable("job_task", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.JobTaskType).HasColumnName("job_task_type");
        builder.Property(x => x.WipTotal).HasColumnName("wip_total");
        builder.Property(x => x.JobPostingGroup).HasColumnName("job_posting_group");
        builder.Property(x => x.WipMethod).HasColumnName("wip_method");
        builder.Property(x => x.Totaling).HasColumnName("totaling");
        builder.Property(x => x.NewPage).HasColumnName("new_page");
        builder.Property(x => x.NoOfBlankLines).HasColumnName("no_of_blank_lines");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
        builder.Property(x => x.RecognizedSalesAmount).HasColumnName("recognized_sales_amount").HasPrecision(18, 5);
        builder.Property(x => x.RecognizedCostsAmount).HasColumnName("recognized_costs_amount").HasPrecision(18, 5);
        builder.Property(x => x.RecognizedSalesGLAmount).HasColumnName("recognized_sales_g_l_amount").HasPrecision(18, 5);
        builder.Property(x => x.RecognizedCostsGLAmount).HasColumnName("recognized_costs_g_l_amount").HasPrecision(18, 5);
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
    }
}
