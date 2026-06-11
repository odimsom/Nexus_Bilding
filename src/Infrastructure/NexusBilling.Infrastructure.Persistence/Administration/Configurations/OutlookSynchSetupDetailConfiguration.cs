using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OutlookSynchSetupDetailConfiguration : IEntityTypeConfiguration<OutlookSynchSetupDetail>
{
    public void Configure(EntityTypeBuilder<OutlookSynchSetupDetail> builder)
    {
        builder.ToTable("outlook_synch_setup_detail", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SynchEntityCode).HasColumnName("synch_entity_code");
        builder.Property(x => x.ElementNo).HasColumnName("element_no");
        builder.Property(x => x.TableNo).HasColumnName("table_no");
    }
}
