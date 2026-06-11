using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class PaymentTermTranslationConfiguration : IEntityTypeConfiguration<PaymentTermTranslation>
{
    public void Configure(EntityTypeBuilder<PaymentTermTranslation> builder)
    {
        builder.ToTable("payment_term_translation", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PaymentTerm).HasColumnName("payment_term");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
