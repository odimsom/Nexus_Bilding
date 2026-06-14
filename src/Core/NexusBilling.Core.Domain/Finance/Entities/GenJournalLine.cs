using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GenJournalLine : Entity
{
    private GenJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; } = string.Empty;
    public int LineNo { get; private set; }
    public short AccountType { get; private set; }
    public string AccountNo { get; private set; } = string.Empty;
    public string? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Vat { get; private set; }
    public string BalAccountNo { get; private set; } = string.Empty;
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
    public string BillToPayToNo { get; private set; } = string.Empty;
    public string PostingGroup { get; private set; } = string.Empty;
    public string ShortcutDimension1Code { get; private set; } = string.Empty;
    public string ShortcutDimension2Code { get; private set; } = string.Empty;
    public string? SalespersPurchCode { get; private set; }
    public string SourceCode { get; private set; } = string.Empty;
    public bool SystemCreatedEntry { get; private set; }
    public string OnHold { get; private set; } = string.Empty;
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; } = string.Empty;
    public string? DueDate { get; private set; }
    public string? PmtDiscountDate { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public string? JobNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal VatAmount { get; private set; }
    public short VatPosting { get; private set; }
    public string PaymentTermsCode { get; private set; } = string.Empty;
    public string AppliesToId { get; private set; } = string.Empty;
    public string BusinessUnitCode { get; private set; } = string.Empty;
    public string JournalBatchName { get; private set; } = string.Empty;
    public string ReasonCode { get; private set; } = string.Empty;
    public short RecurringMethod { get; private set; }
    public string? ExpirationDate { get; private set; }
    public string RecurringFrequency { get; private set; } = string.Empty;
    public short GenPostingType { get; private set; }
    public string GenBusPostingGroup { get; private set; } = string.Empty;
    public string GenProdPostingGroup { get; private set; } = string.Empty;
    public short VatCalculationType { get; private set; }
    public bool Eu3PartyTrade { get; private set; }
    public bool AllowApplication { get; private set; }
    public short BalAccountType { get; private set; }
    public short BalGenPostingType { get; private set; }
    public string BalGenBusPostingGroup { get; private set; } = string.Empty;
    public string BalGenProdPostingGroup { get; private set; } = string.Empty;
    public short BalVatCalculationType { get; private set; }
    public decimal BalVat { get; private set; }
    public decimal BalVatAmount { get; private set; }
    public short BankPaymentType { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal BalVatBaseAmount { get; private set; }
    public bool Correction { get; private set; }
    public bool CheckPrinted { get; private set; }
    public string? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; } = string.Empty;
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; } = string.Empty;
    public string PostingNoSeries { get; private set; } = string.Empty;
    public string TaxAreaCode { get; private set; } = string.Empty;
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; } = string.Empty;
    public bool UseTax { get; private set; }
    public string BalTaxAreaCode { get; private set; } = string.Empty;
    public bool BalTaxLiable { get; private set; }
    public string BalTaxGroupCode { get; private set; } = string.Empty;
    public bool BalUseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; } = string.Empty;
    public string VatProdPostingGroup { get; private set; } = string.Empty;
    public string BalVatBusPostingGroup { get; private set; } = string.Empty;
    public string BalVatProdPostingGroup { get; private set; } = string.Empty;
    public short AdditionalCurrencyPosting { get; private set; }
    public decimal FaAddCurrencyFactor { get; private set; }
    public string? SourceCurrencyCode { get; private set; }
    public decimal SourceCurrencyAmount { get; private set; }
    public decimal SourceCurrVatBaseAmount { get; private set; }
    public decimal SourceCurrVatAmount { get; private set; }
    public decimal VatBaseDiscount { get; private set; }
    public decimal VatAmountLcy { get; private set; }
    public decimal VatBaseAmountLcy { get; private set; }
    public decimal BalVatAmountLcy { get; private set; }
    public decimal BalVatBaseAmountLcy { get; private set; }
    public bool ReversingEntry { get; private set; }
    public bool AllowZeroAmountPosting { get; private set; }
    public string ShipToOrderAddressCode { get; private set; } = string.Empty;
    public decimal VatDifference { get; private set; }
    public decimal BalVatDifference { get; private set; }
    public string IcPartnerCode { get; private set; } = string.Empty;
    public short IcDirection { get; private set; }
    public string IcPartnerGLAccNo { get; private set; } = string.Empty;
    public int IcPartnerTransactionNo { get; private set; }
    public string SellToBuyFromNo { get; private set; } = string.Empty;
    public string VatRegistrationNo { get; private set; } = string.Empty;
    public string? CountryRegionCode { get; private set; }
    public bool Prepayment { get; private set; }
    public bool FinancialVoid { get; private set; }
    public int IncomingDocumentEntryNo { get; private set; }
    public string CreditorNo { get; private set; } = string.Empty;
    public string PaymentReference { get; private set; } = string.Empty;
    public string PaymentMethodCode { get; private set; } = string.Empty;
    public string AppliesToExtDocNo { get; private set; } = string.Empty;
    public string RecipientBankAccount { get; private set; } = string.Empty;
    public string MessageToRecipient { get; private set; } = string.Empty;
    public bool ExportedToPaymentFile { get; private set; }
    public int DimensionSetId { get; private set; }
    public string CreditCardNo { get; private set; } = string.Empty;
    public string JobTaskNo { get; private set; } = string.Empty;
    public decimal JobUnitPriceLcy { get; private set; }
    public decimal JobTotalPriceLcy { get; private set; }
    public decimal JobQuantity { get; private set; }
    public decimal JobUnitCostLcy { get; private set; }
    public decimal JobLineDiscount { get; private set; }
    public decimal JobLineDiscAmountLcy { get; private set; }
    public string JobUnitOfMeasureCode { get; private set; } = string.Empty;
    public short JobLineType { get; private set; }
    public decimal JobUnitPrice { get; private set; }
    public decimal JobTotalPrice { get; private set; }
    public decimal JobUnitCost { get; private set; }
    public decimal JobTotalCost { get; private set; }
    public decimal JobLineDiscountAmount { get; private set; }
    public decimal JobLineAmount { get; private set; }
    public decimal JobTotalCostLcy { get; private set; }
    public decimal JobLineAmountLcy { get; private set; }
    public decimal JobCurrencyFactor { get; private set; }
    public string JobCurrencyCode { get; private set; } = string.Empty;
    public int JobPlanningLineNo { get; private set; }
    public decimal JobRemainingQty { get; private set; }
    public string DirectDebitMandateId { get; private set; } = string.Empty;
    public int DataExchEntryNo { get; private set; }
    public string PayerInformation { get; private set; } = string.Empty;
    public string TransactionInformation { get; private set; } = string.Empty;
    public int DataExchLineNo { get; private set; }
    public bool AppliedAutomatically { get; private set; }
    public string DeferralCode { get; private set; } = string.Empty;
    public int DeferralLineNo { get; private set; }
    public string? CampaignNo { get; private set; }
    public string ProdOrderNo { get; private set; } = string.Empty;
    public string? FaPostingDate { get; private set; }
    public short FaPostingType { get; private set; }
    public string DepreciationBookCode { get; private set; } = string.Empty;
    public decimal SalvageValue { get; private set; }
    public int NoOfDepreciationDays { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public bool DeprAcquisitionCost { get; private set; }
    public string? MaintenanceCode { get; private set; }
    public string? InsuranceNo { get; private set; }
    public string BudgetedFaNo { get; private set; } = string.Empty;
    public string DuplicateInDepreciationBook { get; private set; } = string.Empty;
    public bool UseDuplicationList { get; private set; }
    public bool FaReclassificationEntry { get; private set; }
    public int FaErrorEntryNo { get; private set; }
    public bool IndexEntry { get; private set; }
    public int SourceLineNo { get; private set; }
    public string Comment { get; private set; } = string.Empty;

    public static OperationResult<GenJournalLine, DomainError> Create(
        TenantIdentifier tenantId,
        string journalTemplateName,
        int lineNo,
        short accountType,
        string accountNo,
        string? postingDate,
        short documentType,
        string documentNo,
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
        bool systemCreatedEntry,
        string onHold,
        short appliesToDocType,
        string appliesToDocNo,
        string? dueDate,
        string? pmtDiscountDate,
        decimal paymentDiscount,
        string? jobNo,
        decimal quantity,
        decimal vatAmount,
        short vatPosting,
        string paymentTermsCode,
        string appliesToId,
        string businessUnitCode,
        string journalBatchName,
        string reasonCode,
        short recurringMethod,
        string? expirationDate,
        string recurringFrequency,
        short genPostingType,
        string genBusPostingGroup,
        string genProdPostingGroup,
        short vatCalculationType,
        bool eu3PartyTrade,
        bool allowApplication,
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
        bool checkPrinted,
        string? documentDate,
        string externalDocumentNo,
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
        short additionalCurrencyPosting,
        decimal faAddCurrencyFactor,
        string? sourceCurrencyCode,
        decimal sourceCurrencyAmount,
        decimal sourceCurrVatBaseAmount,
        decimal sourceCurrVatAmount,
        decimal vatBaseDiscount,
        decimal vatAmountLcy,
        decimal vatBaseAmountLcy,
        decimal balVatAmountLcy,
        decimal balVatBaseAmountLcy,
        bool reversingEntry,
        bool allowZeroAmountPosting,
        string shipToOrderAddressCode,
        decimal vatDifference,
        decimal balVatDifference,
        string icPartnerCode,
        short icDirection,
        string icPartnerGLAccNo,
        int icPartnerTransactionNo,
        string sellToBuyFromNo,
        string vatRegistrationNo,
        string? countryRegionCode,
        bool prepayment,
        bool financialVoid,
        int incomingDocumentEntryNo,
        string creditorNo,
        string paymentReference,
        string paymentMethodCode,
        string appliesToExtDocNo,
        string recipientBankAccount,
        string messageToRecipient,
        bool exportedToPaymentFile,
        int dimensionSetId,
        string creditCardNo,
        string jobTaskNo,
        decimal jobUnitPriceLcy,
        decimal jobTotalPriceLcy,
        decimal jobQuantity,
        decimal jobUnitCostLcy,
        decimal jobLineDiscount,
        decimal jobLineDiscAmountLcy,
        string jobUnitOfMeasureCode,
        short jobLineType,
        decimal jobUnitPrice,
        decimal jobTotalPrice,
        decimal jobUnitCost,
        decimal jobTotalCost,
        decimal jobLineDiscountAmount,
        decimal jobLineAmount,
        decimal jobTotalCostLcy,
        decimal jobLineAmountLcy,
        decimal jobCurrencyFactor,
        string jobCurrencyCode,
        int jobPlanningLineNo,
        decimal jobRemainingQty,
        string directDebitMandateId,
        int dataExchEntryNo,
        string payerInformation,
        string transactionInformation,
        int dataExchLineNo,
        bool appliedAutomatically,
        string deferralCode,
        int deferralLineNo,
        string? campaignNo,
        string prodOrderNo,
        string? faPostingDate,
        short faPostingType,
        string depreciationBookCode,
        decimal salvageValue,
        int noOfDepreciationDays,
        bool deprUntilFaPostingDate,
        bool deprAcquisitionCost,
        string? maintenanceCode,
        string? insuranceNo,
        string budgetedFaNo,
        string duplicateInDepreciationBook,
        bool useDuplicationList,
        bool faReclassificationEntry,
        int faErrorEntryNo,
        bool indexEntry,
        int sourceLineNo,
        string comment)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenJournalLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalTemplateName))
            return OperationResult<GenJournalLine, DomainError>.Fail(DomainError.Validation("finance.journal_template_name_required", "El campo journal_template_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(accountNo))
            return OperationResult<GenJournalLine, DomainError>.Fail(DomainError.Validation("finance.account_no_required", "El campo account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(documentNo))
            return OperationResult<GenJournalLine, DomainError>.Fail(DomainError.Validation("finance.document_no_required", "El campo document_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GenJournalLine, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));

        var entity = new GenJournalLine()
        {
            TenantId = tenantId,
            JournalTemplateName = journalTemplateName.Trim(),
            LineNo = lineNo,
            AccountType = accountType,
            AccountNo = accountNo.Trim(),
            PostingDate = postingDate,
            DocumentType = documentType,
            DocumentNo = documentNo.Trim(),
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
            SystemCreatedEntry = systemCreatedEntry,
            OnHold = onHold.Trim(),
            AppliesToDocType = appliesToDocType,
            AppliesToDocNo = appliesToDocNo.Trim(),
            DueDate = dueDate,
            PmtDiscountDate = pmtDiscountDate,
            PaymentDiscount = paymentDiscount,
            JobNo = jobNo,
            Quantity = quantity,
            VatAmount = vatAmount,
            VatPosting = vatPosting,
            PaymentTermsCode = paymentTermsCode.Trim(),
            AppliesToId = appliesToId.Trim(),
            BusinessUnitCode = businessUnitCode.Trim(),
            JournalBatchName = journalBatchName.Trim(),
            ReasonCode = reasonCode.Trim(),
            RecurringMethod = recurringMethod,
            ExpirationDate = expirationDate,
            RecurringFrequency = recurringFrequency.Trim(),
            GenPostingType = genPostingType,
            GenBusPostingGroup = genBusPostingGroup.Trim(),
            GenProdPostingGroup = genProdPostingGroup.Trim(),
            VatCalculationType = vatCalculationType,
            Eu3PartyTrade = eu3PartyTrade,
            AllowApplication = allowApplication,
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
            CheckPrinted = checkPrinted,
            DocumentDate = documentDate,
            ExternalDocumentNo = externalDocumentNo.Trim(),
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
            AdditionalCurrencyPosting = additionalCurrencyPosting,
            FaAddCurrencyFactor = faAddCurrencyFactor,
            SourceCurrencyCode = sourceCurrencyCode,
            SourceCurrencyAmount = sourceCurrencyAmount,
            SourceCurrVatBaseAmount = sourceCurrVatBaseAmount,
            SourceCurrVatAmount = sourceCurrVatAmount,
            VatBaseDiscount = vatBaseDiscount,
            VatAmountLcy = vatAmountLcy,
            VatBaseAmountLcy = vatBaseAmountLcy,
            BalVatAmountLcy = balVatAmountLcy,
            BalVatBaseAmountLcy = balVatBaseAmountLcy,
            ReversingEntry = reversingEntry,
            AllowZeroAmountPosting = allowZeroAmountPosting,
            ShipToOrderAddressCode = shipToOrderAddressCode.Trim(),
            VatDifference = vatDifference,
            BalVatDifference = balVatDifference,
            IcPartnerCode = icPartnerCode.Trim(),
            IcDirection = icDirection,
            IcPartnerGLAccNo = icPartnerGLAccNo.Trim(),
            IcPartnerTransactionNo = icPartnerTransactionNo,
            SellToBuyFromNo = sellToBuyFromNo.Trim(),
            VatRegistrationNo = vatRegistrationNo.Trim(),
            CountryRegionCode = countryRegionCode,
            Prepayment = prepayment,
            FinancialVoid = financialVoid,
            IncomingDocumentEntryNo = incomingDocumentEntryNo,
            CreditorNo = creditorNo.Trim(),
            PaymentReference = paymentReference.Trim(),
            PaymentMethodCode = paymentMethodCode.Trim(),
            AppliesToExtDocNo = appliesToExtDocNo.Trim(),
            RecipientBankAccount = recipientBankAccount.Trim(),
            MessageToRecipient = messageToRecipient.Trim(),
            ExportedToPaymentFile = exportedToPaymentFile,
            DimensionSetId = dimensionSetId,
            CreditCardNo = creditCardNo.Trim(),
            JobTaskNo = jobTaskNo.Trim(),
            JobUnitPriceLcy = jobUnitPriceLcy,
            JobTotalPriceLcy = jobTotalPriceLcy,
            JobQuantity = jobQuantity,
            JobUnitCostLcy = jobUnitCostLcy,
            JobLineDiscount = jobLineDiscount,
            JobLineDiscAmountLcy = jobLineDiscAmountLcy,
            JobUnitOfMeasureCode = jobUnitOfMeasureCode.Trim(),
            JobLineType = jobLineType,
            JobUnitPrice = jobUnitPrice,
            JobTotalPrice = jobTotalPrice,
            JobUnitCost = jobUnitCost,
            JobTotalCost = jobTotalCost,
            JobLineDiscountAmount = jobLineDiscountAmount,
            JobLineAmount = jobLineAmount,
            JobTotalCostLcy = jobTotalCostLcy,
            JobLineAmountLcy = jobLineAmountLcy,
            JobCurrencyFactor = jobCurrencyFactor,
            JobCurrencyCode = jobCurrencyCode.Trim(),
            JobPlanningLineNo = jobPlanningLineNo,
            JobRemainingQty = jobRemainingQty,
            DirectDebitMandateId = directDebitMandateId.Trim(),
            DataExchEntryNo = dataExchEntryNo,
            PayerInformation = payerInformation.Trim(),
            TransactionInformation = transactionInformation.Trim(),
            DataExchLineNo = dataExchLineNo,
            AppliedAutomatically = appliedAutomatically,
            DeferralCode = deferralCode.Trim(),
            DeferralLineNo = deferralLineNo,
            CampaignNo = campaignNo,
            ProdOrderNo = prodOrderNo.Trim(),
            FaPostingDate = faPostingDate,
            FaPostingType = faPostingType,
            DepreciationBookCode = depreciationBookCode.Trim(),
            SalvageValue = salvageValue,
            NoOfDepreciationDays = noOfDepreciationDays,
            DeprUntilFaPostingDate = deprUntilFaPostingDate,
            DeprAcquisitionCost = deprAcquisitionCost,
            MaintenanceCode = maintenanceCode,
            InsuranceNo = insuranceNo,
            BudgetedFaNo = budgetedFaNo.Trim(),
            DuplicateInDepreciationBook = duplicateInDepreciationBook.Trim(),
            UseDuplicationList = useDuplicationList,
            FaReclassificationEntry = faReclassificationEntry,
            FaErrorEntryNo = faErrorEntryNo,
            IndexEntry = indexEntry,
            SourceLineNo = sourceLineNo,
            Comment = comment.Trim(),
        };

        return OperationResult<GenJournalLine, DomainError>.Ok(entity);
    }
}
