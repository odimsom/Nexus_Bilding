using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class LotNoInformationConfiguration : IEntityTypeConfiguration<LotNoInformation>
{
    public void Configure(EntityTypeBuilder<LotNoInformation> builder)
    {
        builder.ToTable("lot_no_information", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TestQuality).HasColumnName("test_quality");
        builder.Property(x => x.CertificateNumber).HasColumnName("certificate_number");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
    }
}
