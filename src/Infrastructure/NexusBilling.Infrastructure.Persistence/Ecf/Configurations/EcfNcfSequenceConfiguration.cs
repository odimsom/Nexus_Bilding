using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Infrastructure.Persistence.Ecf.Configurations;

public class EcfNcfSequenceConfiguration : IEntityTypeConfiguration<EcfNcfSequence>
{
    public void Configure(EntityTypeBuilder<EcfNcfSequence> builder)
    {
        builder.ToTable("ecf_ncf_sequence", "erp");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasConversion(v => v.Value, v => TenantIdentifier.Create(v))
            .HasColumnName("tenant_id")
            .IsRequired();

        builder.Property(e => e.NcfType)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.CurrentNumber)
            .IsRequired();

        builder.Property(e => e.MaxNumber)
            .IsRequired();

        builder.Property(e => e.ExpirationDate)
            .IsRequired();
            
        builder.HasIndex(e => new { e.TenantId, e.NcfType }).IsUnique();
    }
}
