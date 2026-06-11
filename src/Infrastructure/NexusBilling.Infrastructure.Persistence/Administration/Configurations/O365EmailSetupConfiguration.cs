using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class O365EmailSetupConfiguration : IEntityTypeConfiguration<O365EmailSetup>
{
    public void Configure(EntityTypeBuilder<O365EmailSetup> builder)
    {
        builder.ToTable("o365_email_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Recipienttype).HasColumnName("recipienttype");
    }
}
