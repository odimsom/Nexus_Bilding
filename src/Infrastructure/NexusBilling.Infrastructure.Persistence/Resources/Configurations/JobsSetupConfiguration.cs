using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobsSetupConfiguration : IEntityTypeConfiguration<JobsSetup>
{
    public void Configure(EntityTypeBuilder<JobsSetup> builder)
    {
        builder.ToTable("jobs_setup", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.JobNos).HasColumnName("job_nos");
        builder.Property(x => x.ApplyUsageLinkByDefault).HasColumnName("apply_usage_link_by_default");
        builder.Property(x => x.DefaultWipMethod).HasColumnName("default_wip_method");
        builder.Property(x => x.DefaultJobPostingGroup).HasColumnName("default_job_posting_group");
        builder.Property(x => x.DefaultWipPostingMethod).HasColumnName("default_wip_posting_method");
        builder.Property(x => x.AllowSchedContractLinesDef).HasColumnName("allow_sched_contract_lines_def");
        builder.Property(x => x.LogoPositionOnDocuments).HasColumnName("logo_position_on_documents");
        builder.Property(x => x.JobWipNos).HasColumnName("job_wip_nos");
        builder.Property(x => x.AutomaticUpdateJobItemCost).HasColumnName("automatic_update_job_item_cost");
    }
}
