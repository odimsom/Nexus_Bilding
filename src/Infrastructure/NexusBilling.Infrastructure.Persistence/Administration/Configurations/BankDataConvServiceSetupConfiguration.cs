using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankDataConvServiceSetupConfiguration : IEntityTypeConfiguration<BankDataConvServiceSetup>
{
    public void Configure(EntityTypeBuilder<BankDataConvServiceSetup> builder)
    {
        builder.ToTable("bank_data_conv_service_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.PasswordKey).HasColumnName("password_key");
        builder.Property(x => x.SignUpUrl).HasColumnName("sign_up_url");
        builder.Property(x => x.ServiceUrl).HasColumnName("service_url");
        builder.Property(x => x.SupportUrl).HasColumnName("support_url");
    }
}
