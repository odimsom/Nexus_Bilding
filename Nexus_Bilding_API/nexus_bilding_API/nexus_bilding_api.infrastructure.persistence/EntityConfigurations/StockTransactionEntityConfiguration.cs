using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nexus_bilding_api.core.domain.Entities;

namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;

public class StockTransactionEntityConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.ToTable("StockTransactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Reference)
            .HasMaxLength(100);

        builder.Property(t => t.Type)
            .HasConversion<string>();

        // Relationships
        builder.HasOne(t => t.Product)
            .WithMany(p => p.StockTransactions)
            .HasForeignKey(t => t.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
