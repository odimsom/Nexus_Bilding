using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class VatRegistrationLogConfiguration : IEntityTypeConfiguration<VatRegistrationLog>
{
    public void Configure(EntityTypeBuilder<VatRegistrationLog> builder)
    {
        builder.ToTable("vat_registration_log", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.AccountType).HasColumnName("account_type");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.VerifiedName).HasColumnName("verified_name");
        builder.Property(x => x.VerifiedAddress).HasColumnName("verified_address");
        builder.Property(x => x.VerifiedDate).HasColumnName("verified_date");
    }
}
