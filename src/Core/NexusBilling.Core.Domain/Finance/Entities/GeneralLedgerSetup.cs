using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GeneralLedgerSetup : Entity
{
    private GeneralLedgerSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string? AllowPostingFrom { get; private set; }
    public string? AllowPostingTo { get; private set; }
    public bool RegisterTime { get; private set; }
    public bool PmtDiscExclVat { get; private set; }
    public bool UnrealizedVat { get; private set; }
    public bool AdjustForPaymentDisc { get; private set; }
    public bool MarkCrMemosAsCorrections { get; private set; }
    public short LocalAddressFormat { get; private set; }
    public decimal InvRoundingPrecisionLcy { get; private set; }
    public short InvRoundingTypeLcy { get; private set; }
    public short LocalContAddrFormat { get; private set; }
    public string BankAccountNos { get; private set; }
    public bool SummarizeGLEntries { get; private set; }
    public string AmountDecimalPlaces { get; private set; }
    public string UnitAmountDecimalPlaces { get; private set; }
    public string? AdditionalReportingCurrency { get; private set; }
    public decimal VatTolerance { get; private set; }
    public bool EmuCurrency { get; private set; }
    public string LcyCode { get; private set; }
    public short VatExchangeRateAdjustment { get; private set; }
    public decimal AmountRoundingPrecision { get; private set; }
    public decimal UnitAmountRoundingPrecision { get; private set; }
    public decimal ApplnRoundingPrecision { get; private set; }
    public string? GlobalDimension1Code { get; private set; }
    public string? GlobalDimension2Code { get; private set; }
    public string? ShortcutDimension1Code { get; private set; }
    public string? ShortcutDimension2Code { get; private set; }
    public string? ShortcutDimension3Code { get; private set; }
    public string? ShortcutDimension4Code { get; private set; }
    public string? ShortcutDimension5Code { get; private set; }
    public string? ShortcutDimension6Code { get; private set; }
    public string? ShortcutDimension7Code { get; private set; }
    public string? ShortcutDimension8Code { get; private set; }
    public decimal MaxVatDifferenceAllowed { get; private set; }
    public short VatRoundingType { get; private set; }
    public short PmtDiscTolerancePosting { get; private set; }
    public string PaymentDiscountGracePeriod { get; private set; }
    public decimal PaymentTolerance { get; private set; }
    public decimal MaxPaymentToleranceAmount { get; private set; }
    public bool AdaptMainMenuToPermissions { get; private set; }
    public string? AllowGLAccDeletionBefore { get; private set; }
    public bool CheckGLAccountUsage { get; private set; }
    public short PaymentTolerancePosting { get; private set; }
    public bool PmtDiscToleranceWarning { get; private set; }
    public bool PaymentToleranceWarning { get; private set; }
    public int LastIcTransactionNo { get; private set; }
    public short BillToSellToVatCalc { get; private set; }
    public string AccSchedForBalanceSheet { get; private set; }
    public string AccSchedForIncomeStmt { get; private set; }
    public string AccSchedForCashFlowStmt { get; private set; }
    public string AccSchedForRetainedEarn { get; private set; }
    public bool PrintVatSpecificationInLcy { get; private set; }
    public bool PrepaymentUnrealizedVat { get; private set; }
    public bool UseLegacyGLEntryLocking { get; private set; }
    public string PayrollTransImportFormat { get; private set; }
    public string VatRegNoValidationUrl { get; private set; }
    public string LocalCurrencySymbol { get; private set; }

    public static OperationResult<GeneralLedgerSetup, DomainError> Create(
        TenantIdentifier tenantId,
        string primaryKey,
        string? allowPostingFrom,
        string? allowPostingTo,
        bool registerTime,
        bool pmtDiscExclVat,
        bool unrealizedVat,
        bool adjustForPaymentDisc,
        bool markCrMemosAsCorrections,
        short localAddressFormat,
        decimal invRoundingPrecisionLcy,
        short invRoundingTypeLcy,
        short localContAddrFormat,
        string bankAccountNos,
        bool summarizeGLEntries,
        string amountDecimalPlaces,
        string unitAmountDecimalPlaces,
        string? additionalReportingCurrency,
        decimal vatTolerance,
        bool emuCurrency,
        string lcyCode,
        short vatExchangeRateAdjustment,
        decimal amountRoundingPrecision,
        decimal unitAmountRoundingPrecision,
        decimal applnRoundingPrecision,
        string? globalDimension1Code,
        string? globalDimension2Code,
        string? shortcutDimension1Code,
        string? shortcutDimension2Code,
        string? shortcutDimension3Code,
        string? shortcutDimension4Code,
        string? shortcutDimension5Code,
        string? shortcutDimension6Code,
        string? shortcutDimension7Code,
        string? shortcutDimension8Code,
        decimal maxVatDifferenceAllowed,
        short vatRoundingType,
        short pmtDiscTolerancePosting,
        string paymentDiscountGracePeriod,
        decimal paymentTolerance,
        decimal maxPaymentToleranceAmount,
        bool adaptMainMenuToPermissions,
        string? allowGLAccDeletionBefore,
        bool checkGLAccountUsage,
        short paymentTolerancePosting,
        bool pmtDiscToleranceWarning,
        bool paymentToleranceWarning,
        int lastIcTransactionNo,
        short billToSellToVatCalc,
        string accSchedForBalanceSheet,
        string accSchedForIncomeStmt,
        string accSchedForCashFlowStmt,
        string accSchedForRetainedEarn,
        bool printVatSpecificationInLcy,
        bool prepaymentUnrealizedVat,
        bool useLegacyGLEntryLocking,
        string payrollTransImportFormat,
        string vatRegNoValidationUrl,
        string localCurrencySymbol)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(primaryKey))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.primary_key_required", "El campo primary_key es obligatorio."));
        if (string.IsNullOrWhiteSpace(bankAccountNos))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.bank_account_nos_required", "El campo bank_account_nos es obligatorio."));
        if (string.IsNullOrWhiteSpace(amountDecimalPlaces))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.amount_decimal_places_required", "El campo amount_decimal_places es obligatorio."));
        if (string.IsNullOrWhiteSpace(unitAmountDecimalPlaces))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.unit_amount_decimal_places_required", "El campo unit_amount_decimal_places es obligatorio."));
        if (string.IsNullOrWhiteSpace(lcyCode))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.lcy_code_required", "El campo lcy_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(paymentDiscountGracePeriod))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.payment_discount_grace_period_required", "El campo payment_discount_grace_period es obligatorio."));
        if (string.IsNullOrWhiteSpace(accSchedForBalanceSheet))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.acc_sched_for_balance_sheet_required", "El campo acc_sched_for_balance_sheet es obligatorio."));
        if (string.IsNullOrWhiteSpace(accSchedForIncomeStmt))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.acc_sched_for_income_stmt_required", "El campo acc_sched_for_income_stmt es obligatorio."));
        if (string.IsNullOrWhiteSpace(accSchedForCashFlowStmt))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.acc_sched_for_cash_flow_stmt_required", "El campo acc_sched_for_cash_flow_stmt es obligatorio."));
        if (string.IsNullOrWhiteSpace(accSchedForRetainedEarn))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.acc_sched_for_retained_earn_required", "El campo acc_sched_for_retained_earn es obligatorio."));
        if (string.IsNullOrWhiteSpace(payrollTransImportFormat))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.payroll_trans_import_format_required", "El campo payroll_trans_import_format es obligatorio."));
        if (string.IsNullOrWhiteSpace(vatRegNoValidationUrl))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.vat_reg_no_validation_url_required", "El campo vat_reg_no_validation_url es obligatorio."));
        if (string.IsNullOrWhiteSpace(localCurrencySymbol))
            return OperationResult<GeneralLedgerSetup, DomainError>.Fail(DomainError.Validation("finance.local_currency_symbol_required", "El campo local_currency_symbol es obligatorio."));

        var entity = new GeneralLedgerSetup()
        {
            TenantId = tenantId,
            PrimaryKey = primaryKey.Trim(),
            AllowPostingFrom = allowPostingFrom,
            AllowPostingTo = allowPostingTo,
            RegisterTime = registerTime,
            PmtDiscExclVat = pmtDiscExclVat,
            UnrealizedVat = unrealizedVat,
            AdjustForPaymentDisc = adjustForPaymentDisc,
            MarkCrMemosAsCorrections = markCrMemosAsCorrections,
            LocalAddressFormat = localAddressFormat,
            InvRoundingPrecisionLcy = invRoundingPrecisionLcy,
            InvRoundingTypeLcy = invRoundingTypeLcy,
            LocalContAddrFormat = localContAddrFormat,
            BankAccountNos = bankAccountNos.Trim(),
            SummarizeGLEntries = summarizeGLEntries,
            AmountDecimalPlaces = amountDecimalPlaces.Trim(),
            UnitAmountDecimalPlaces = unitAmountDecimalPlaces.Trim(),
            AdditionalReportingCurrency = additionalReportingCurrency,
            VatTolerance = vatTolerance,
            EmuCurrency = emuCurrency,
            LcyCode = lcyCode.Trim(),
            VatExchangeRateAdjustment = vatExchangeRateAdjustment,
            AmountRoundingPrecision = amountRoundingPrecision,
            UnitAmountRoundingPrecision = unitAmountRoundingPrecision,
            ApplnRoundingPrecision = applnRoundingPrecision,
            GlobalDimension1Code = globalDimension1Code,
            GlobalDimension2Code = globalDimension2Code,
            ShortcutDimension1Code = shortcutDimension1Code,
            ShortcutDimension2Code = shortcutDimension2Code,
            ShortcutDimension3Code = shortcutDimension3Code,
            ShortcutDimension4Code = shortcutDimension4Code,
            ShortcutDimension5Code = shortcutDimension5Code,
            ShortcutDimension6Code = shortcutDimension6Code,
            ShortcutDimension7Code = shortcutDimension7Code,
            ShortcutDimension8Code = shortcutDimension8Code,
            MaxVatDifferenceAllowed = maxVatDifferenceAllowed,
            VatRoundingType = vatRoundingType,
            PmtDiscTolerancePosting = pmtDiscTolerancePosting,
            PaymentDiscountGracePeriod = paymentDiscountGracePeriod.Trim(),
            PaymentTolerance = paymentTolerance,
            MaxPaymentToleranceAmount = maxPaymentToleranceAmount,
            AdaptMainMenuToPermissions = adaptMainMenuToPermissions,
            AllowGLAccDeletionBefore = allowGLAccDeletionBefore,
            CheckGLAccountUsage = checkGLAccountUsage,
            PaymentTolerancePosting = paymentTolerancePosting,
            PmtDiscToleranceWarning = pmtDiscToleranceWarning,
            PaymentToleranceWarning = paymentToleranceWarning,
            LastIcTransactionNo = lastIcTransactionNo,
            BillToSellToVatCalc = billToSellToVatCalc,
            AccSchedForBalanceSheet = accSchedForBalanceSheet.Trim(),
            AccSchedForIncomeStmt = accSchedForIncomeStmt.Trim(),
            AccSchedForCashFlowStmt = accSchedForCashFlowStmt.Trim(),
            AccSchedForRetainedEarn = accSchedForRetainedEarn.Trim(),
            PrintVatSpecificationInLcy = printVatSpecificationInLcy,
            PrepaymentUnrealizedVat = prepaymentUnrealizedVat,
            UseLegacyGLEntryLocking = useLegacyGLEntryLocking,
            PayrollTransImportFormat = payrollTransImportFormat.Trim(),
            VatRegNoValidationUrl = vatRegNoValidationUrl.Trim(),
            LocalCurrencySymbol = localCurrencySymbol.Trim(),
        };

        return OperationResult<GeneralLedgerSetup, DomainError>.Ok(entity);
    }
}
