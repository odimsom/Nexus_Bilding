using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class FaultCodeConfiguration : IEntityTypeConfiguration<FaultCode>
{
    public void Configure(EntityTypeBuilder<FaultCode> builder)
    {
        builder.ToTable("fault_code", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaultAreaCode).HasColumnName("fault_area_code");
        builder.Property(x => x.SymptomCode).HasColumnName("symptom_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
