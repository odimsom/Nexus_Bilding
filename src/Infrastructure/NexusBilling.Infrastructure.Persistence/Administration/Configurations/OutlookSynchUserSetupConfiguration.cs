using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OutlookSynchUserSetupConfiguration : IEntityTypeConfiguration<OutlookSynchUserSetup>
{
    public void Configure(EntityTypeBuilder<OutlookSynchUserSetup> builder)
    {
        builder.ToTable("outlook_synch_user_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SynchEntityCode).HasColumnName("synch_entity_code");
        builder.Property(x => x.Condition).HasColumnName("condition");
        builder.Property(x => x.SynchDirection).HasColumnName("synch_direction");
        builder.Property(x => x.LastSynchTime).HasColumnName("last_synch_time");
        builder.Property(x => x.RecordGuid).HasColumnName("record_guid");
    }
}
