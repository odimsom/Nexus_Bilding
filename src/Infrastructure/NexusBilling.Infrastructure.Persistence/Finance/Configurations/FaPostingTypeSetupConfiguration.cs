using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaPostingTypeSetupConfiguration : IEntityTypeConfiguration<FaPostingTypeSetup>
{
    public void Configure(EntityTypeBuilder<FaPostingTypeSetup> builder)
    {
        builder.ToTable("fa_posting_type_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaPostingType).HasColumnName("fa_posting_type");
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.PartOfBookValue).HasColumnName("part_of_book_value");
        builder.Property(x => x.PartOfDepreciableBasis).HasColumnName("part_of_depreciable_basis");
        builder.Property(x => x.IncludeInDeprCalculation).HasColumnName("include_in_depr_calculation");
        builder.Property(x => x.IncludeInGainLossCalc).HasColumnName("include_in_gain_loss_calc");
        builder.Property(x => x.ReverseBeforeDisposal).HasColumnName("reverse_before_disposal");
        builder.Property(x => x.Sign).HasColumnName("sign");
        builder.Property(x => x.DepreciationType).HasColumnName("depreciation_type");
        builder.Property(x => x.AcquisitionType).HasColumnName("acquisition_type");
    }
}
