using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaSetupConfiguration : IEntityTypeConfiguration<FaSetup>
{
    public void Configure(EntityTypeBuilder<FaSetup> builder)
    {
        builder.ToTable("fa_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AllowPostingToMainAssets).HasColumnName("allow_posting_to_main_assets");
        builder.Property(x => x.DefaultDeprBook).HasColumnName("default_depr_book");
        builder.Property(x => x.AllowFaPostingFrom).HasColumnName("allow_fa_posting_from");
        builder.Property(x => x.AllowFaPostingTo).HasColumnName("allow_fa_posting_to");
        builder.Property(x => x.InsuranceDeprBook).HasColumnName("insurance_depr_book");
        builder.Property(x => x.AutomaticInsurancePosting).HasColumnName("automatic_insurance_posting");
        builder.Property(x => x.FixedAssetNos).HasColumnName("fixed_asset_nos");
        builder.Property(x => x.InsuranceNos).HasColumnName("insurance_nos");
    }
}
