using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseJournalBatchConfiguration : IEntityTypeConfiguration<WarehouseJournalBatch>
{
    public void Configure(EntityTypeBuilder<WarehouseJournalBatch> builder)
    {
        builder.ToTable("warehouse_journal_batch", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.RegisteringNoSeries).HasColumnName("registering_no_series");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
    }
}
