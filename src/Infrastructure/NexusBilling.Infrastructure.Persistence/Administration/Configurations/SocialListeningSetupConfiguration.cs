using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SocialListeningSetupConfiguration : IEntityTypeConfiguration<SocialListeningSetup>
{
    public void Configure(EntityTypeBuilder<SocialListeningSetup> builder)
    {
        builder.ToTable("social_listening_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.SolutionId).HasColumnName("solution_id");
        builder.Property(x => x.ShowOnItems).HasColumnName("show_on_items");
        builder.Property(x => x.ShowOnCustomers).HasColumnName("show_on_customers");
        builder.Property(x => x.ShowOnVendors).HasColumnName("show_on_vendors");
        builder.Property(x => x.AcceptLicenseAgreement).HasColumnName("accept_license_agreement");
        builder.Property(x => x.TermsOfUseUrl).HasColumnName("terms_of_use_url");
        builder.Property(x => x.SignupUrl).HasColumnName("signup_url");
        builder.Property(x => x.SocialListeningUrl).HasColumnName("social_listening_url");
    }
}
