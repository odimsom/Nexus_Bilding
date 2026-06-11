using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BaseCalendarChangeConfiguration : IEntityTypeConfiguration<BaseCalendarChange>
{
    public void Configure(EntityTypeBuilder<BaseCalendarChange> builder)
    {
        builder.ToTable("base_calendar_change", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.BaseCalendarCode).HasColumnName("base_calendar_code");
        builder.Property(x => x.RecurringSystem).HasColumnName("recurring_system");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Day).HasColumnName("day");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Nonworking).HasColumnName("nonworking");
    }
}
