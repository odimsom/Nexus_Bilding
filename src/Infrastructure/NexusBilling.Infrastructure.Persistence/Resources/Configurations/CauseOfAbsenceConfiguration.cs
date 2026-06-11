using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class CauseOfAbsenceConfiguration : IEntityTypeConfiguration<CauseOfAbsence>
{
    public void Configure(EntityTypeBuilder<CauseOfAbsence> builder)
    {
        builder.ToTable("cause_of_absence", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
    }
}
