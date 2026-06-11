using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class RelationshipMgmtCueConfiguration : IEntityTypeConfiguration<RelationshipMgmtCue>
{
    public void Configure(EntityTypeBuilder<RelationshipMgmtCue> builder)
    {
        builder.ToTable("relationship_mgmt_cue", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
    }
}
