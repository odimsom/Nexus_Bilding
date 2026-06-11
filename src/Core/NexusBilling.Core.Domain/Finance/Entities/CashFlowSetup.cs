using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CashFlowSetup : Entity
{
    private CashFlowSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string CashFlowForecastNoSeries { get; private set; }
    public string ReceivablesCfAccountNo { get; private set; }
    public string PayablesCfAccountNo { get; private set; }
    public string SalesOrderCfAccountNo { get; private set; }
    public string PurchOrderCfAccountNo { get; private set; }
    public string FaBudgetCfAccountNo { get; private set; }
    public string FaDisposalCfAccountNo { get; private set; }
    public string ServiceCfAccountNo { get; private set; }
    public string CfNoOnChartInRoleCenter { get; private set; }
    public string JobCfAccountNo { get; private set; }
    public short AutomaticUpdateFrequency { get; private set; }
    public string TaxCfAccountNo { get; private set; }
    public short TaxablePeriod { get; private set; }
    public string TaxPaymentWindow { get; private set; }
    public short TaxBalAccountType { get; private set; }
    public string TaxBalAccountNo { get; private set; }
    public string ApiKey { get; private set; }
    public string ApiUrl { get; private set; }
    public int Variance { get; private set; }
    public int HistoricalPeriods { get; private set; }
    public int Horizon { get; private set; }
    public short PeriodType { get; private set; }
    public int Timeout { get; private set; }
    public Guid ServicePassApiKeyId { get; private set; }
    public bool CortanaIntelligenceEnabled { get; private set; }
    public bool ShowCortanaNotification { get; private set; }

    public static OperationResult<CashFlowSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CashFlowSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CashFlowSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CashFlowSetup, DomainError>.Ok(entity);
    }
}
