using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class EmployeeAbsenceConfiguration : IEntityTypeConfiguration<EmployeeAbsence>
{
    public void Configure(EntityTypeBuilder<EmployeeAbsence> builder)
    {
        builder.ToTable("employee_absence", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EmployeeNo).HasColumnName("employee_no");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.FromDate).HasColumnName("from_date");
        builder.Property(x => x.ToDate).HasColumnName("to_date");
        builder.Property(x => x.CauseOfAbsenceCode).HasColumnName("cause_of_absence_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
    }
}
