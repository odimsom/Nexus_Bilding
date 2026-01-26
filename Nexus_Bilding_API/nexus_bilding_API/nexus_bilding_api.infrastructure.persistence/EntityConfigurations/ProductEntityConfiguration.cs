using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Sku)
            .HasMaxLength(50);

        builder.Property(p => p.Category)
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Cost)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.ExemptionReason)
            .HasMaxLength(200);

        builder.Property(p => p.ItbisRate)
            .HasConversion<string>();

        builder.Property(p => p.Status)
            .HasConversion<string>();

        // Indexes
        builder.HasIndex(p => p.Sku)
            .IsUnique()
            .HasFilter("[Sku] IS NOT NULL"); // Depending on DB provider, but usually good for optional unique columns
            
        // Relationships
        builder.HasMany(p => p.StockTransactions)
            .WithOne(t => t.Product)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
