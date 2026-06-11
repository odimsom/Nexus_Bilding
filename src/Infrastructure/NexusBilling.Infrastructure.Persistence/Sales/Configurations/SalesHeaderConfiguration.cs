using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesHeaderConfiguration : IEntityTypeConfiguration<SalesHeader>
{
    public void Configure(EntityTypeBuilder<SalesHeader> builder)
    {
        builder.ToTable("sales_header", "sales");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();

        builder.Property(x => x.DocumentType).IsRequired();
        builder.Property(x => x.No).IsRequired().HasMaxLength(20);
        builder.Property(x => x.SellToCustomerNo).HasMaxLength(20);
        builder.Property(x => x.BillToName).HasMaxLength(50);
        builder.Property(x => x.PostingDate).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.DocumentType, x.No }).IsUnique();
    }
}
