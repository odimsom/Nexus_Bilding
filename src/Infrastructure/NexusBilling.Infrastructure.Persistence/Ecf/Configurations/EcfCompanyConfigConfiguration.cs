using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Infrastructure.Persistence.Ecf.Configurations;

public class EcfCompanyConfigConfiguration : IEntityTypeConfiguration<EcfCompanyConfig>
{
    public void Configure(EntityTypeBuilder<EcfCompanyConfig> builder)
    {
        builder.ToTable("ecf_company_config", "erp");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasConversion(v => v.Value, v => TenantIdentifier.Create(v))
            .HasColumnName("tenant_id")
            .IsRequired();

        builder.Property(e => e.Rnc)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.RepresentativeName)
            .HasMaxLength(200);

        builder.Property(e => e.P12Path)
            .HasMaxLength(500);

        builder.Property(e => e.P12PasswordHash)
            .HasMaxLength(500);
            
        builder.Property(e => e.Environment)
            .HasConversion<int>();
    }
}
