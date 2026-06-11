using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class BusinessUnit : Entity
{
    private BusinessUnit() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public bool Consolidate { get; private set; }
    public decimal Consolidation { get; private set; }
    public string? StartingDate { get; private set; }
    public string? EndingDate { get; private set; }
    public decimal IncomeCurrencyFactor { get; private set; }
    public decimal BalanceCurrencyFactor { get; private set; }
    public string ExchRateLossesAcc { get; private set; }
    public string ExchRateGainsAcc { get; private set; }
    public string ResidualAccount { get; private set; }
    public decimal LastBalanceCurrencyFactor { get; private set; }
    public string Name { get; private set; }
    public string CompanyName { get; private set; }
    public string? CurrencyCode { get; private set; }
    public string CompExchRateGainsAcc { get; private set; }
    public string CompExchRateLossesAcc { get; private set; }
    public string EquityExchRateGainsAcc { get; private set; }
    public string EquityExchRateLossesAcc { get; private set; }
    public string MinorityExchRateGainsAcc { get; private set; }
    public string MinorityExchRateLossesAcc { get; private set; }
    public short CurrencyExchangeRateTable { get; private set; }
    public short DataSource { get; private set; }
    public short FileFormat { get; private set; }

    public static OperationResult<BusinessUnit, DomainError> Create(
        TenantIdentifier tenantId,
        string code,
        bool consolidate,
        decimal consolidation,
        string? startingDate,
        string? endingDate,
        decimal incomeCurrencyFactor,
        decimal balanceCurrencyFactor,
        string exchRateLossesAcc,
        string exchRateGainsAcc,
        string residualAccount,
        decimal lastBalanceCurrencyFactor,
        string name,
        string companyName,
        string? currencyCode,
        string compExchRateGainsAcc,
        string compExchRateLossesAcc,
        string equityExchRateGainsAcc,
        string equityExchRateLossesAcc,
        string minorityExchRateGainsAcc,
        string minorityExchRateLossesAcc,
        short currencyExchangeRateTable,
        short dataSource,
        short fileFormat)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.code_required", "El campo code es obligatorio."));
        if (string.IsNullOrWhiteSpace(exchRateLossesAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.exch_rate_losses_acc_required", "El campo exch_rate_losses_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(exchRateGainsAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.exch_rate_gains_acc_required", "El campo exch_rate_gains_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(residualAccount))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.residual_account_required", "El campo residual_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));
        if (string.IsNullOrWhiteSpace(companyName))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.company_name_required", "El campo company_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(compExchRateGainsAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.comp_exch_rate_gains_acc_required", "El campo comp_exch_rate_gains_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(compExchRateLossesAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.comp_exch_rate_losses_acc_required", "El campo comp_exch_rate_losses_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(equityExchRateGainsAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.equity_exch_rate_gains_acc_required", "El campo equity_exch_rate_gains_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(equityExchRateLossesAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.equity_exch_rate_losses_acc_required", "El campo equity_exch_rate_losses_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(minorityExchRateGainsAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.minority_exch_rate_gains_acc_required", "El campo minority_exch_rate_gains_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(minorityExchRateLossesAcc))
            return OperationResult<BusinessUnit, DomainError>.Fail(DomainError.Validation("finance.minority_exch_rate_losses_acc_required", "El campo minority_exch_rate_losses_acc es obligatorio."));

        var entity = new BusinessUnit()
        {
            TenantId = tenantId,
            Code = code.Trim(),
            Consolidate = consolidate,
            Consolidation = consolidation,
            StartingDate = startingDate,
            EndingDate = endingDate,
            IncomeCurrencyFactor = incomeCurrencyFactor,
            BalanceCurrencyFactor = balanceCurrencyFactor,
            ExchRateLossesAcc = exchRateLossesAcc.Trim(),
            ExchRateGainsAcc = exchRateGainsAcc.Trim(),
            ResidualAccount = residualAccount.Trim(),
            LastBalanceCurrencyFactor = lastBalanceCurrencyFactor,
            Name = name.Trim(),
            CompanyName = companyName.Trim(),
            CurrencyCode = currencyCode,
            CompExchRateGainsAcc = compExchRateGainsAcc.Trim(),
            CompExchRateLossesAcc = compExchRateLossesAcc.Trim(),
            EquityExchRateGainsAcc = equityExchRateGainsAcc.Trim(),
            EquityExchRateLossesAcc = equityExchRateLossesAcc.Trim(),
            MinorityExchRateGainsAcc = minorityExchRateGainsAcc.Trim(),
            MinorityExchRateLossesAcc = minorityExchRateLossesAcc.Trim(),
            CurrencyExchangeRateTable = currencyExchangeRateTable,
            DataSource = dataSource,
            FileFormat = fileFormat,
        };

        return OperationResult<BusinessUnit, DomainError>.Ok(entity);
    }
}
