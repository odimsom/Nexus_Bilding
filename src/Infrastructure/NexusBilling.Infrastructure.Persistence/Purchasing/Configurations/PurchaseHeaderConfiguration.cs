using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class PurchaseHeaderConfiguration : IEntityTypeConfiguration<PurchaseHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseHeader> builder)
    {
        builder.ToTable("purchase_header", "purchasing");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();

        builder.Property(x => x.DocumentType).IsRequired();
        builder.Property(x => x.No).IsRequired().HasMaxLength(20);
        builder.Property(x => x.BuyFromVendorNo).HasMaxLength(20);
        builder.Property(x => x.PayToName).HasMaxLength(50);
        builder.Property(x => x.PostingDate).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.DocumentType, x.No }).IsUnique();
    }
}
