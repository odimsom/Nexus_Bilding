using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class LoanerConfiguration : IEntityTypeConfiguration<Loaner>
{
    public void Configure(EntityTypeBuilder<Loaner> builder)
    {
        builder.ToTable("loaner", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
    }
}
