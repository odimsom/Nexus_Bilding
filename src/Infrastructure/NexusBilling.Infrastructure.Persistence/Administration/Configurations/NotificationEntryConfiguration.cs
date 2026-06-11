using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NotificationEntryConfiguration : IEntityTypeConfiguration<NotificationEntry>
{
    public void Configure(EntityTypeBuilder<NotificationEntry> builder)
    {
        builder.ToTable("notification_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.RecipientUserId).HasColumnName("recipient_user_id");
        builder.Property(x => x.TriggeredByRecord).HasColumnName("triggered_by_record");
        builder.Property(x => x.LinkTargetPage).HasColumnName("link_target_page");
        builder.Property(x => x.CustomLink).HasColumnName("custom_link");
        builder.Property(x => x.ErrorMessage).HasColumnName("error_message");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
        builder.Property(x => x.ErrorMessage2).HasColumnName("error_message_2");
        builder.Property(x => x.ErrorMessage3).HasColumnName("error_message_3");
        builder.Property(x => x.ErrorMessage4).HasColumnName("error_message_4");
    }
}
