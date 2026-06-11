using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class RepairStatusConfiguration : IEntityTypeConfiguration<RepairStatus>
{
    public void Configure(EntityTypeBuilder<RepairStatus> builder)
    {
        builder.ToTable("repair_status", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ServiceOrderStatus).HasColumnName("service_order_status");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.Initial).HasColumnName("initial");
        builder.Property(x => x.PartlyServiced).HasColumnName("partly_serviced");
        builder.Property(x => x.InProcess).HasColumnName("in_process");
        builder.Property(x => x.Finished).HasColumnName("finished");
        builder.Property(x => x.Referred).HasColumnName("referred");
        builder.Property(x => x.SparePartOrdered).HasColumnName("spare_part_ordered");
        builder.Property(x => x.SparePartReceived).HasColumnName("spare_part_received");
        builder.Property(x => x.WaitingForCustomer).HasColumnName("waiting_for_customer");
        builder.Property(x => x.QuoteFinished).HasColumnName("quote_finished");
        builder.Property(x => x.PostingAllowed).HasColumnName("posting_allowed");
        builder.Property(x => x.PendingStatusAllowed).HasColumnName("pending_status_allowed");
        builder.Property(x => x.InProcessStatusAllowed).HasColumnName("in_process_status_allowed");
        builder.Property(x => x.FinishedStatusAllowed).HasColumnName("finished_status_allowed");
        builder.Property(x => x.OnHoldStatusAllowed).HasColumnName("on_hold_status_allowed");
    }
}
