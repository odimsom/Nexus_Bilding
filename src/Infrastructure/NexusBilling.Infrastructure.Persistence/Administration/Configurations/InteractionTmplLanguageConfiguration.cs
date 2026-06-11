using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InteractionTmplLanguageConfiguration : IEntityTypeConfiguration<InteractionTmplLanguage>
{
    public void Configure(EntityTypeBuilder<InteractionTmplLanguage> builder)
    {
        builder.ToTable("interaction_tmpl_language", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.InteractionTemplateCode).HasColumnName("interaction_template_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AttachmentNo).HasColumnName("attachment_no");
        builder.Property(x => x.CustomLayoutCode).HasColumnName("custom_layout_code");
    }
}
