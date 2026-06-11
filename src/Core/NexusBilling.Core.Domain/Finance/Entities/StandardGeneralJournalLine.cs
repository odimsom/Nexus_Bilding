using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class StandardGeneralJournalLine : Entity
{
    private StandardGeneralJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public int LineNo { get; private set; }
    public short AccountType { get; private set; }
    public string AccountNo { get; private set; }
    public short DocumentType { get; private set; }
    public string Description { get; private set; }
    public decimal Vat { get; private set; }
    public string BalAccountNo { get; private set; }
    public string? CurrencyCode { get; private set; }
    public decimal Amount { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal AmountLcy { get; private set; }
    public decimal BalanceLcy { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public decimal SalesPurchLcy { get; private set; }
    public decimal ProfitLcy { get; private set; }
    public decimal InvDiscountLcy { get; private set; }
    public string BillToPayToNo { get; private set; }
    public string PostingGroup { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string? SalespersPurchCode { get; private set; }
    public string SourceCode { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public string? JobNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal VatAmount { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public string BusinessUnitCode { get; private set; }
    public string StandardJournalCode { get; private set; }
    public string ReasonCode { get; private set; }
    public short GenPostingType { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public short VatCalculationType { get; private set; }
    public short BalAccountType { get; private set; }
    public short BalGenPostingType { get; private set; }
    public string BalGenBusPostingGroup { get; private set; }
    public string BalGenProdPostingGroup { get; private set; }
    public short BalVatCalculationType { get; private set; }
    public decimal BalVat { get; private set; }
    public decimal BalVatAmount { get; private set; }
    public short BankPaymentType { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal BalVatBaseAmount { get; private set; }
    public bool Correction { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public string PostingNoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string BalTaxAreaCode { get; private set; }
    public bool BalTaxLiable { get; private set; }
    public string BalTaxGroupCode { get; private set; }
    public bool BalUseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string BalVatBusPostingGroup { get; private set; }
    public string BalVatProdPostingGroup { get; private set; }
    public string ShipToOrderAddressCode { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal BalVatDifference { get; private set; }
    public string IcPartnerCode { get; private set; }
    public string IcPartnerGLAccNo { get; private set; }
    public string SellToBuyFromNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public string? CampaignNo { get; private set; }
    public bool IndexEntry { get; private set; }

    public static OperationResult<StandardGeneralJournalLine, DomainError> Create(
        TenantIdentifier tenantId,
        string journalTemplateName,
        int lineNo,
        short accountType,
        string accountNo,
        short documentType,
        string description,
        decimal vat,
        string balAccountNo,
        string? currencyCode,
        decimal amount,
        decimal debitAmount,
        decimal creditAmount,
        decimal amountLcy,
        decimal balanceLcy,
        decimal currencyFactor,
        decimal salesPurchLcy,
        decimal profitLcy,
        decimal invDiscountLcy,
        string billToPayToNo,
        string postingGroup,
        string shortcutDimension1Code,
        string shortcutDimension2Code,
        string? salespersPurchCode,
        string sourceCode,
        string onHold,
        short appliesToDocType,
        decimal paymentDiscount,
        string? jobNo,
        decimal quantity,
        decimal vatAmount,
        string paymentTermsCode,
        string businessUnitCode,
        string standardJournalCode,
        string reasonCode,
        short genPostingType,
        string genBusPostingGroup,
        string genProdPostingGroup,
        short vatCalculationType,
        short balAccountType,
        short balGenPostingType,
        string balGenBusPostingGroup,
        string balGenProdPostingGroup,
        short balVatCalculationType,
        decimal balVat,
        decimal balVatAmount,
        short bankPaymentType,
        decimal vatBaseAmount,
        decimal balVatBaseAmount,
        bool correction,
        short sourceType,
        string sourceNo,
        string postingNoSeries,
        string taxAreaCode,
        bool taxLiable,
        string taxGroupCode,
        bool useTax,
        string balTaxAreaCode,
        bool balTaxLiable,
        string balTaxGroupCode,
        bool balUseTax,
        string vatBusPostingGroup,
        string vatProdPostingGroup,
        string balVatBusPostingGroup,
        string balVatProdPostingGroup,
        string shipToOrderAddressCode,
        decimal vatDifference,
        decimal balVatDifference,
        string icPartnerCode,
        string icPartnerGLAccNo,
        string sellToBuyFromNo,
        int dimensionSetId,
        string? campaignNo,
        bool indexEntry)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalTemplateName))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.journal_template_name_required", "El campo journal_template_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(accountNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.account_no_required", "El campo account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(balAccountNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_account_no_required", "El campo bal_account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(billToPayToNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bill_to_pay_to_no_required", "El campo bill_to_pay_to_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(postingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.posting_group_required", "El campo posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(shortcutDimension1Code))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.shortcut_dimension_1_code_required", "El campo shortcut_dimension_1_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(shortcutDimension2Code))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.shortcut_dimension_2_code_required", "El campo shortcut_dimension_2_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.source_code_required", "El campo source_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(onHold))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.on_hold_required", "El campo on_hold es obligatorio."));
        if (string.IsNullOrWhiteSpace(paymentTermsCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.payment_terms_code_required", "El campo payment_terms_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(businessUnitCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.business_unit_code_required", "El campo business_unit_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(standardJournalCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.standard_journal_code_required", "El campo standard_journal_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(reasonCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.reason_code_required", "El campo reason_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(genBusPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.gen_bus_posting_group_required", "El campo gen_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(genProdPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.gen_prod_posting_group_required", "El campo gen_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(balGenBusPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_gen_bus_posting_group_required", "El campo bal_gen_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(balGenProdPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_gen_prod_posting_group_required", "El campo bal_gen_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.source_no_required", "El campo source_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(postingNoSeries))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.posting_no_series_required", "El campo posting_no_series es obligatorio."));
        if (string.IsNullOrWhiteSpace(taxAreaCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.tax_area_code_required", "El campo tax_area_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(taxGroupCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.tax_group_code_required", "El campo tax_group_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(balTaxAreaCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_tax_area_code_required", "El campo bal_tax_area_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(balTaxGroupCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_tax_group_code_required", "El campo bal_tax_group_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(vatBusPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.vat_bus_posting_group_required", "El campo vat_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(vatProdPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.vat_prod_posting_group_required", "El campo vat_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(balVatBusPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_vat_bus_posting_group_required", "El campo bal_vat_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(balVatProdPostingGroup))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.bal_vat_prod_posting_group_required", "El campo bal_vat_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(shipToOrderAddressCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.ship_to_order_address_code_required", "El campo ship_to_order_address_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(icPartnerCode))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.ic_partner_code_required", "El campo ic_partner_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(icPartnerGLAccNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.ic_partner_g_l_acc_no_required", "El campo ic_partner_g_l_acc_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(sellToBuyFromNo))
            return OperationResult<StandardGeneralJournalLine, DomainError>.Fail(DomainError.Validation("finance.sell_to_buy_from_no_required", "El campo sell_to_buy_from_no es obligatorio."));

        var entity = new StandardGeneralJournalLine()
        {
            TenantId = tenantId,
            JournalTemplateName = journalTemplateName.Trim(),
            LineNo = lineNo,
            AccountType = accountType,
            AccountNo = accountNo.Trim(),
            DocumentType = documentType,
            Description = description.Trim(),
            Vat = vat,
            BalAccountNo = balAccountNo.Trim(),
            CurrencyCode = currencyCode,
            Amount = amount,
            DebitAmount = debitAmount,
            CreditAmount = creditAmount,
            AmountLcy = amountLcy,
            BalanceLcy = balanceLcy,
            CurrencyFactor = currencyFactor,
            SalesPurchLcy = salesPurchLcy,
            ProfitLcy = profitLcy,
            InvDiscountLcy = invDiscountLcy,
            BillToPayToNo = billToPayToNo.Trim(),
            PostingGroup = postingGroup.Trim(),
            ShortcutDimension1Code = shortcutDimension1Code.Trim(),
            ShortcutDimension2Code = shortcutDimension2Code.Trim(),
            SalespersPurchCode = salespersPurchCode,
            SourceCode = sourceCode.Trim(),
            OnHold = onHold.Trim(),
            AppliesToDocType = appliesToDocType,
            PaymentDiscount = paymentDiscount,
            JobNo = jobNo,
            Quantity = quantity,
            VatAmount = vatAmount,
            PaymentTermsCode = paymentTermsCode.Trim(),
            BusinessUnitCode = businessUnitCode.Trim(),
            StandardJournalCode = standardJournalCode.Trim(),
            ReasonCode = reasonCode.Trim(),
            GenPostingType = genPostingType,
            GenBusPostingGroup = genBusPostingGroup.Trim(),
            GenProdPostingGroup = genProdPostingGroup.Trim(),
            VatCalculationType = vatCalculationType,
            BalAccountType = balAccountType,
            BalGenPostingType = balGenPostingType,
            BalGenBusPostingGroup = balGenBusPostingGroup.Trim(),
            BalGenProdPostingGroup = balGenProdPostingGroup.Trim(),
            BalVatCalculationType = balVatCalculationType,
            BalVat = balVat,
            BalVatAmount = balVatAmount,
            BankPaymentType = bankPaymentType,
            VatBaseAmount = vatBaseAmount,
            BalVatBaseAmount = balVatBaseAmount,
            Correction = correction,
            SourceType = sourceType,
            SourceNo = sourceNo.Trim(),
            PostingNoSeries = postingNoSeries.Trim(),
            TaxAreaCode = taxAreaCode.Trim(),
            TaxLiable = taxLiable,
            TaxGroupCode = taxGroupCode.Trim(),
            UseTax = useTax,
            BalTaxAreaCode = balTaxAreaCode.Trim(),
            BalTaxLiable = balTaxLiable,
            BalTaxGroupCode = balTaxGroupCode.Trim(),
            BalUseTax = balUseTax,
            VatBusPostingGroup = vatBusPostingGroup.Trim(),
            VatProdPostingGroup = vatProdPostingGroup.Trim(),
            BalVatBusPostingGroup = balVatBusPostingGroup.Trim(),
            BalVatProdPostingGroup = balVatProdPostingGroup.Trim(),
            ShipToOrderAddressCode = shipToOrderAddressCode.Trim(),
            VatDifference = vatDifference,
            BalVatDifference = balVatDifference,
            IcPartnerCode = icPartnerCode.Trim(),
            IcPartnerGLAccNo = icPartnerGLAccNo.Trim(),
            SellToBuyFromNo = sellToBuyFromNo.Trim(),
            DimensionSetId = dimensionSetId,
            CampaignNo = campaignNo,
            IndexEntry = indexEntry,
        };

        return OperationResult<StandardGeneralJournalLine, DomainError>.Ok(entity);
    }
}
