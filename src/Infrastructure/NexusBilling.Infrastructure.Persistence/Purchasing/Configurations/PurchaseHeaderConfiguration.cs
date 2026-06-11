using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class PurchaseHeaderConfiguration : IEntityTypeConfiguration<PurchaseHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseHeader> builder)
    {
        builder.ToTable("purchase_header", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.BuyFromVendorNo).HasColumnName("buy_from_vendor_no");
        builder.Property(x => x.PayToName).HasColumnName("pay_to_name");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
    }
}
