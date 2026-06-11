using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceOrderAllocationConfiguration : IEntityTypeConfiguration<ServiceOrderAllocation>
{
    public void Configure(EntityTypeBuilder<ServiceOrderAllocation> builder)
    {
        builder.ToTable("service_order_allocation", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.AllocationDate).HasColumnName("allocation_date");
        builder.Property(x => x.ResourceNo).HasColumnName("resource_no");
        builder.Property(x => x.ResourceGroupNo).HasColumnName("resource_group_no");
        builder.Property(x => x.ServiceItemLineNo).HasColumnName("service_item_line_no");
        builder.Property(x => x.AllocatedHours).HasColumnName("allocated_hours").HasPrecision(18, 5);
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.FinishingTime).HasColumnName("finishing_time");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.ServiceItemNo).HasColumnName("service_item_no");
        builder.Property(x => x.Posted).HasColumnName("posted");
        builder.Property(x => x.ServiceItemSerialNo).HasColumnName("service_item_serial_no");
        builder.Property(x => x.ServiceStarted).HasColumnName("service_started");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
    }
}
