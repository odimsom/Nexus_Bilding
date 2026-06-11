using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceItemComponentConfiguration : IEntityTypeConfiguration<ServiceItemComponent>
{
    public void Configure(EntityTypeBuilder<ServiceItemComponent> builder)
    {
        builder.ToTable("service_item_component", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ParentServiceItemNo).HasColumnName("parent_service_item_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Active).HasColumnName("active");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.DateInstalled).HasColumnName("date_installed");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.ServiceOrderNo).HasColumnName("service_order_no");
        builder.Property(x => x.FromLineNo).HasColumnName("from_line_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
    }
}
