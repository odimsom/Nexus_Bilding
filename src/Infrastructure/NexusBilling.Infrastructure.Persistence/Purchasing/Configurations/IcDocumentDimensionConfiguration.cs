using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcDocumentDimensionConfiguration : IEntityTypeConfiguration<IcDocumentDimension>
{
    public void Configure(EntityTypeBuilder<IcDocumentDimension> builder)
    {
        builder.ToTable("ic_document_dimension", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.TransactionSource).HasColumnName("transaction_source");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueCode).HasColumnName("dimension_value_code");
    }
}
