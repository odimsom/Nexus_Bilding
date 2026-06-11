using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PlanningErrorLogConfiguration : IEntityTypeConfiguration<PlanningErrorLog>
{
    public void Configure(EntityTypeBuilder<PlanningErrorLog> builder)
    {
        builder.ToTable("planning_error_log", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.WorksheetTemplateName).HasColumnName("worksheet_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.ErrorDescription).HasColumnName("error_description");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.TablePosition).HasColumnName("table_position");
    }
}
