using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CostJournalBatchConfiguration : IEntityTypeConfiguration<CostJournalBatch>
{
    public void Configure(EntityTypeBuilder<CostJournalBatch> builder)
    {
        builder.ToTable("cost_journal_batch", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.BalCostTypeNo).HasColumnName("bal_cost_type_no");
        builder.Property(x => x.BalCostCenterCode).HasColumnName("bal_cost_center_code");
        builder.Property(x => x.BalCostObjectCode).HasColumnName("bal_cost_object_code");
        builder.Property(x => x.DeleteAfterPosting).HasColumnName("delete_after_posting");
    }
}
