using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class RestrictedRecordConfiguration : IEntityTypeConfiguration<RestrictedRecord>
{
    public void Configure(EntityTypeBuilder<RestrictedRecord> builder)
    {
        builder.ToTable("restricted_record", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
        builder.Property(x => x.Details).HasColumnName("details");
    }
}
