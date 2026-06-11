using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionTranslationConfiguration : IEntityTypeConfiguration<DimensionTranslation>
{
    public void Configure(EntityTypeBuilder<DimensionTranslation> builder)
    {
        builder.ToTable("dimension_translation", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.LanguageId).HasColumnName("language_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CodeCaption).HasColumnName("code_caption");
        builder.Property(x => x.FilterCaption).HasColumnName("filter_caption");
    }
}
