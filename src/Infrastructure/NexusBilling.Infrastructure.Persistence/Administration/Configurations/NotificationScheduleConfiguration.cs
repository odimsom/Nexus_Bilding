using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NotificationScheduleConfiguration : IEntityTypeConfiguration<NotificationSchedule>
{
    public void Configure(EntityTypeBuilder<NotificationSchedule> builder)
    {
        builder.ToTable("notification_schedule", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.NotificationType).HasColumnName("notification_type");
        builder.Property(x => x.Recurrence).HasColumnName("recurrence");
        builder.Property(x => x.Time).HasColumnName("time");
        builder.Property(x => x.DailyFrequency).HasColumnName("daily_frequency");
        builder.Property(x => x.Monday).HasColumnName("monday");
        builder.Property(x => x.Tuesday).HasColumnName("tuesday");
        builder.Property(x => x.Wednesday).HasColumnName("wednesday");
        builder.Property(x => x.Thursday).HasColumnName("thursday");
        builder.Property(x => x.Friday).HasColumnName("friday");
        builder.Property(x => x.Saturday).HasColumnName("saturday");
        builder.Property(x => x.Sunday).HasColumnName("sunday");
        builder.Property(x => x.DateOfMonth).HasColumnName("date_of_month");
        builder.Property(x => x.MonthlyNotificationDate).HasColumnName("monthly_notification_date");
        builder.Property(x => x.LastScheduledJob).HasColumnName("last_scheduled_job");
    }
}
