using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ShipmentMethodTranslationConfiguration : IEntityTypeConfiguration<ShipmentMethodTranslation>
{
    public void Configure(EntityTypeBuilder<ShipmentMethodTranslation> builder)
    {
        builder.ToTable("shipment_method_translation", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ShipmentMethod).HasColumnName("shipment_method");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
