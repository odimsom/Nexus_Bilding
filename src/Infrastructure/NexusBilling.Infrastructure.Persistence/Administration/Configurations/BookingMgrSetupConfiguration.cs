using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BookingMgrSetupConfiguration : IEntityTypeConfiguration<BookingMgrSetup>
{
    public void Configure(EntityTypeBuilder<BookingMgrSetup> builder)
    {
        builder.ToTable("booking_mgr_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.BookingMgrCodeunit).HasColumnName("booking_mgr_codeunit");
    }
}
