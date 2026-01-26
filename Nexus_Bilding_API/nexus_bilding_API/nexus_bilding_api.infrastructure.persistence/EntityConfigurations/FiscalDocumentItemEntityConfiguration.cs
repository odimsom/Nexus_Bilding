using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;

public class FiscalDocumentItemEntityConfiguration : IEntityTypeConfiguration<FiscalDocumentItem>
{
    public void Configure(EntityTypeBuilder<FiscalDocumentItem> builder)
    {
        builder.ToTable("FiscalDocumentItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(i => i.ExemptionReason)
            .HasMaxLength(200);

        builder.Property(i => i.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.ItbisAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Total)
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.ItbisRate)
            .HasConversion<string>();

        // Relationships
        builder.HasOne(i => i.FiscalDocument)
            .WithMany(f => f.Items)
            .HasForeignKey(i => i.FiscalDocumentId)
            .OnDelete(DeleteBehavior.Cascade); // Items belong to a document

        builder.HasOne(i => i.Product)
            .WithMany() // Product doesn't necessarily track all items it's been in explicitly in domain unless needed
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.SetNull); // If product is deleted, keep the history
    }
}
