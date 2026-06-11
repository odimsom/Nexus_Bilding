using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SentNotificationEntryConfiguration : IEntityTypeConfiguration<SentNotificationEntry>
{
    public void Configure(EntityTypeBuilder<SentNotificationEntry> builder)
    {
        builder.ToTable("sent_notification_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.RecipientUserId).HasColumnName("recipient_user_id");
        builder.Property(x => x.TriggeredByRecord).HasColumnName("triggered_by_record");
        builder.Property(x => x.LinkTargetPage).HasColumnName("link_target_page");
        builder.Property(x => x.CustomLink).HasColumnName("custom_link");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
        builder.Property(x => x.SentDateTime).HasColumnName("sent_date_time");
        builder.Property(x => x.NotificationContent).HasColumnName("notification_content");
        builder.Property(x => x.NotificationMethod).HasColumnName("notification_method");
        builder.Property(x => x.AggregatedWithEntry).HasColumnName("aggregated_with_entry");
    }
}
