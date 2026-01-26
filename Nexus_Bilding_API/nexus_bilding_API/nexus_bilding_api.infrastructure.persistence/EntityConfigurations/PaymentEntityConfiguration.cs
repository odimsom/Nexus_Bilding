using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;

public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Method)
            .HasConversion<string>();

        // Relationships
        builder.HasOne(p => p.FiscalDocument)
            .WithMany(f => f.Payments)
            .HasForeignKey(p => p.FiscalDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
