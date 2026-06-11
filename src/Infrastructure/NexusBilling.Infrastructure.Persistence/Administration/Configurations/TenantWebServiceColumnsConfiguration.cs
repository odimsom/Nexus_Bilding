using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantWebServiceColumnsConfiguration : IEntityTypeConfiguration<TenantWebServiceColumns>
{
    public void Configure(EntityTypeBuilder<TenantWebServiceColumns> builder)
    {
        builder.ToTable("tenant_web_service_columns", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryId).HasColumnName("entry_id");
        builder.Property(x => x.DataItem).HasColumnName("data_item");
        builder.Property(x => x.FieldNumber).HasColumnName("field_number");
        builder.Property(x => x.FieldName).HasColumnName("field_name");
        builder.Property(x => x.Tenantwebserviceid).HasColumnName("tenantwebserviceid");
        builder.Property(x => x.Include).HasColumnName("include");
    }
}
