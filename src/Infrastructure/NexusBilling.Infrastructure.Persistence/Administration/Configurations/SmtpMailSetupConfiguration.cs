using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SmtpMailSetupConfiguration : IEntityTypeConfiguration<SmtpMailSetup>
{
    public void Configure(EntityTypeBuilder<SmtpMailSetup> builder)
    {
        builder.ToTable("smtp_mail_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.SmtpServer).HasColumnName("smtp_server");
        builder.Property(x => x.Authentication).HasColumnName("authentication");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SmtpServerPort).HasColumnName("smtp_server_port");
        builder.Property(x => x.SecureConnection).HasColumnName("secure_connection");
        builder.Property(x => x.PasswordKey).HasColumnName("password_key");
    }
}
