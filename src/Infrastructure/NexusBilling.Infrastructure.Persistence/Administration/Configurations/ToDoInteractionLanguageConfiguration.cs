using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ToDoInteractionLanguageConfiguration : IEntityTypeConfiguration<ToDoInteractionLanguage>
{
    public void Configure(EntityTypeBuilder<ToDoInteractionLanguage> builder)
    {
        builder.ToTable("to_do_interaction_language", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ToDoNo).HasColumnName("to_do_no");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AttachmentNo).HasColumnName("attachment_no");
    }
}
