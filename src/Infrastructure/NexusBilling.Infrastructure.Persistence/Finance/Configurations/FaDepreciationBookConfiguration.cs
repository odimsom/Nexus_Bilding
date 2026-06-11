using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaDepreciationBookConfiguration : IEntityTypeConfiguration<FaDepreciationBook>
{
    public void Configure(EntityTypeBuilder<FaDepreciationBook> builder)
    {
        builder.ToTable("fa_depreciation_book", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaNo).HasColumnName("fa_no");
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.DepreciationMethod).HasColumnName("depreciation_method");
        builder.Property(x => x.DepreciationStartingDate).HasColumnName("depreciation_starting_date");
        builder.Property(x => x.StraightLine).HasColumnName("straight_line").HasPrecision(18, 5);
        builder.Property(x => x.NoOfDepreciationYears).HasColumnName("no_of_depreciation_years").HasPrecision(18, 5);
        builder.Property(x => x.NoOfDepreciationMonths).HasColumnName("no_of_depreciation_months").HasPrecision(18, 5);
        builder.Property(x => x.FixedDeprAmount).HasColumnName("fixed_depr_amount").HasPrecision(18, 5);
        builder.Property(x => x.DecliningBalance).HasColumnName("declining_balance").HasPrecision(18, 5);
        builder.Property(x => x.DepreciationTableCode).HasColumnName("depreciation_table_code");
        builder.Property(x => x.FinalRoundingAmount).HasColumnName("final_rounding_amount").HasPrecision(18, 5);
        builder.Property(x => x.EndingBookValue).HasColumnName("ending_book_value").HasPrecision(18, 5);
        builder.Property(x => x.FaPostingGroup).HasColumnName("fa_posting_group");
        builder.Property(x => x.DepreciationEndingDate).HasColumnName("depreciation_ending_date");
        builder.Property(x => x.AcquisitionDate).HasColumnName("acquisition_date");
        builder.Property(x => x.GLAcquisitionDate).HasColumnName("g_l_acquisition_date");
        builder.Property(x => x.DisposalDate).HasColumnName("disposal_date");
        builder.Property(x => x.LastAcquisitionCostDate).HasColumnName("last_acquisition_cost_date");
        builder.Property(x => x.LastDepreciationDate).HasColumnName("last_depreciation_date");
        builder.Property(x => x.LastWriteDownDate).HasColumnName("last_write_down_date");
        builder.Property(x => x.LastAppreciationDate).HasColumnName("last_appreciation_date");
        builder.Property(x => x.LastCustom1Date).HasColumnName("last_custom_1_date");
        builder.Property(x => x.LastCustom2Date).HasColumnName("last_custom_2_date");
        builder.Property(x => x.LastSalvageValueDate).HasColumnName("last_salvage_value_date");
        builder.Property(x => x.FaExchangeRate).HasColumnName("fa_exchange_rate").HasPrecision(18, 5);
        builder.Property(x => x.FixedDeprAmountBelowZero).HasColumnName("fixed_depr_amount_below_zero").HasPrecision(18, 5);
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.FirstUserDefinedDeprDate).HasColumnName("first_user_defined_depr_date");
        builder.Property(x => x.UseFaLedgerCheck).HasColumnName("use_fa_ledger_check");
        builder.Property(x => x.LastMaintenanceDate).HasColumnName("last_maintenance_date");
        builder.Property(x => x.DeprBelowZero).HasColumnName("depr_below_zero").HasPrecision(18, 5);
        builder.Property(x => x.ProjectedDisposalDate).HasColumnName("projected_disposal_date");
        builder.Property(x => x.ProjectedProceedsOnDisposal).HasColumnName("projected_proceeds_on_disposal").HasPrecision(18, 5);
        builder.Property(x => x.DeprStartingDateCustom1).HasColumnName("depr_starting_date_custom_1");
        builder.Property(x => x.DeprEndingDateCustom1).HasColumnName("depr_ending_date_custom_1");
        builder.Property(x => x.AccumDeprCustom1).HasColumnName("accum_depr_custom_1").HasPrecision(18, 5);
        builder.Property(x => x.DeprThisYearCustom1).HasColumnName("depr_this_year_custom_1").HasPrecision(18, 5);
        builder.Property(x => x.PropertyClassCustom1).HasColumnName("property_class_custom_1");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.MainAssetComponent).HasColumnName("main_asset_component");
        builder.Property(x => x.ComponentOfMainAsset).HasColumnName("component_of_main_asset");
        builder.Property(x => x.FaAddCurrencyFactor).HasColumnName("fa_add_currency_factor").HasPrecision(18, 5);
        builder.Property(x => x.UseHalfYearConvention).HasColumnName("use_half_year_convention");
        builder.Property(x => x.UseDbFirstFiscalYear).HasColumnName("use_db_first_fiscal_year");
        builder.Property(x => x.TempEndingDate).HasColumnName("temp_ending_date");
        builder.Property(x => x.TempFixedDeprAmount).HasColumnName("temp_fixed_depr_amount").HasPrecision(18, 5);
        builder.Property(x => x.IgnoreDefEndingBookValue).HasColumnName("ignore_def_ending_book_value");
        builder.Property(x => x.DefaultFaDepreciationBook).HasColumnName("default_fa_depreciation_book");
    }
}
