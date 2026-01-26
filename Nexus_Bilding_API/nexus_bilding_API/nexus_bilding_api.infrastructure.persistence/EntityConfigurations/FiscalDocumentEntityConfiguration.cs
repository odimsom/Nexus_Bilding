using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;

public class FiscalDocumentEntityConfiguration : IEntityTypeConfiguration<FiscalDocument>
{
    public void Configure(EntityTypeBuilder<FiscalDocument> builder)
    {
        builder.ToTable("FiscalDocuments");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.ECFNumber)
            .HasMaxLength(20);

        builder.Property(f => f.ClientName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.ClientTaxId)
            .HasMaxLength(20);

        builder.Property(f => f.SecurityCode)
            .HasMaxLength(100);

        builder.Property(f => f.XmlUrl)
            .HasMaxLength(500);

        builder.Property(f => f.QrCodeContent)
            .HasMaxLength(1000); // Assuming QR content can be long-ish

        builder.Property(f => f.Subtotal)
            .HasColumnType("decimal(18,2)");

        builder.Property(f => f.TotalITBIS)
            .HasColumnType("decimal(18,2)");

        builder.Property(f => f.TotalDiscount)
            .HasColumnType("decimal(18,2)");

        builder.Property(f => f.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(f => f.DocumentType)
            .HasConversion<string>();

        builder.Property(f => f.Status)
            .HasConversion<string>();
            
        builder.Property(f => f.Notes)
            .HasMaxLength(1000);

        // Relationships
        builder.HasOne(f => f.Client)
            .WithMany(c => c.FiscalDocuments)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.SetNull); // Or Restrict/Cascade depending on business rule, assuming SetNull or Restrict if keeping history is important. Client entity config used Restrict, so this side is implicitly matching the FK constraint.

        builder.HasMany(f => f.Items)
            .WithOne(i => i.FiscalDocument)
            .HasForeignKey(i => i.FiscalDocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Payments)
            .WithOne(p => p.FiscalDocument)
            .HasForeignKey(p => p.FiscalDocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
