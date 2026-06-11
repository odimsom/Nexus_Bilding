using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CashFlowSetupConfiguration : IEntityTypeConfiguration<CashFlowSetup>
{
    public void Configure(EntityTypeBuilder<CashFlowSetup> builder)
    {
        builder.ToTable("cash_flow_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.CashFlowForecastNoSeries).HasColumnName("cash_flow_forecast_no_series");
        builder.Property(x => x.ReceivablesCfAccountNo).HasColumnName("receivables_cf_account_no");
        builder.Property(x => x.PayablesCfAccountNo).HasColumnName("payables_cf_account_no");
        builder.Property(x => x.SalesOrderCfAccountNo).HasColumnName("sales_order_cf_account_no");
        builder.Property(x => x.PurchOrderCfAccountNo).HasColumnName("purch_order_cf_account_no");
        builder.Property(x => x.FaBudgetCfAccountNo).HasColumnName("fa_budget_cf_account_no");
        builder.Property(x => x.FaDisposalCfAccountNo).HasColumnName("fa_disposal_cf_account_no");
        builder.Property(x => x.ServiceCfAccountNo).HasColumnName("service_cf_account_no");
        builder.Property(x => x.CfNoOnChartInRoleCenter).HasColumnName("cf_no_on_chart_in_role_center");
        builder.Property(x => x.JobCfAccountNo).HasColumnName("job_cf_account_no");
        builder.Property(x => x.AutomaticUpdateFrequency).HasColumnName("automatic_update_frequency");
        builder.Property(x => x.TaxCfAccountNo).HasColumnName("tax_cf_account_no");
        builder.Property(x => x.TaxablePeriod).HasColumnName("taxable_period");
        builder.Property(x => x.TaxPaymentWindow).HasColumnName("tax_payment_window");
        builder.Property(x => x.TaxBalAccountType).HasColumnName("tax_bal_account_type");
        builder.Property(x => x.TaxBalAccountNo).HasColumnName("tax_bal_account_no");
        builder.Property(x => x.ApiKey).HasColumnName("api_key");
        builder.Property(x => x.ApiUrl).HasColumnName("api_url");
        builder.Property(x => x.Variance).HasColumnName("variance");
        builder.Property(x => x.HistoricalPeriods).HasColumnName("historical_periods");
        builder.Property(x => x.Horizon).HasColumnName("horizon");
        builder.Property(x => x.PeriodType).HasColumnName("period_type");
        builder.Property(x => x.Timeout).HasColumnName("timeout");
        builder.Property(x => x.ServicePassApiKeyId).HasColumnName("service_pass_api_key_id");
        builder.Property(x => x.CortanaIntelligenceEnabled).HasColumnName("cortana_intelligence_enabled");
        builder.Property(x => x.ShowCortanaNotification).HasColumnName("show_cortana_notification");
    }
}
