using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class UnitOfMeasureTranslationConfiguration : IEntityTypeConfiguration<UnitOfMeasureTranslation>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasureTranslation> builder)
    {
        builder.ToTable("unit_of_measure_translation", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
