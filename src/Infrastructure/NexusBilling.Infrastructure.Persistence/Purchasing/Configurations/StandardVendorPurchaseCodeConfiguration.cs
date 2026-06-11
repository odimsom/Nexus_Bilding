using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class StandardVendorPurchaseCodeConfiguration : IEntityTypeConfiguration<StandardVendorPurchaseCode>
{
    public void Configure(EntityTypeBuilder<StandardVendorPurchaseCode> builder)
    {
        builder.ToTable("standard_vendor_purchase_code", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
