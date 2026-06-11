using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLEntry : Entity
{
    private GLEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string GLAccountNo { get; private set; }
    public string? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string BalAccountNo { get; private set; }
    public decimal Amount { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public bool PriorYearEntry { get; private set; }
    public string? JobNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal VatAmount { get; private set; }
    public string BusinessUnitCode { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short GenPostingType { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public short BalAccountType { get; private set; }
    public int TransactionNo { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public string? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public string NoSeries { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public decimal AdditionalCurrencyAmount { get; private set; }
    public decimal AddCurrencyDebitAmount { get; private set; }
    public decimal AddCurrencyCreditAmount { get; private set; }
    public int CloseIncomeStatementDimId { get; private set; }
    public string IcPartnerCode { get; private set; }
    public bool Reversed { get; private set; }
    public int ReversedByEntryNo { get; private set; }
    public int ReversedEntryNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ProdOrderNo { get; private set; }
    public short FaEntryType { get; private set; }
    public int FaEntryNo { get; private set; }

    public static OperationResult<GLEntry, DomainError> Create(
        TenantIdentifier tenantId,
        int entryNo,
        string gLAccountNo,
        string? postingDate,
        short documentType,
        string documentNo,
        string description,
        string balAccountNo,
        decimal amount,
        string globalDimension1Code,
        string globalDimension2Code,
        string userId,
        string sourceCode,
        bool systemCreatedEntry,
        bool priorYearEntry,
        string? jobNo,
        decimal quantity,
        decimal vatAmount,
        string businessUnitCode,
        string journalBatchName,
        string reasonCode,
        short genPostingType,
        string genBusPostingGroup,
        string genProdPostingGroup,
        short balAccountType,
        int transactionNo,
        decimal debitAmount,
        decimal creditAmount,
        string? documentDate,
        string externalDocumentNo,
        short sourceType,
        string sourceNo,
        string noSeries,
        string taxAreaCode,
        bool taxLiable,
        string taxGroupCode,
        bool useTax,
        string vatBusPostingGroup,
        string vatProdPostingGroup,
        decimal additionalCurrencyAmount,
        decimal addCurrencyDebitAmount,
        decimal addCurrencyCreditAmount,
        int closeIncomeStatementDimId,
        string icPartnerCode,
        bool reversed,
        int reversedByEntryNo,
        int reversedEntryNo,
        int dimensionSetId,
        string prodOrderNo,
        short faEntryType,
        int faEntryNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(gLAccountNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.g_l_account_no_required", "El campo g_l_account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(documentNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.document_no_required", "El campo document_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(balAccountNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.bal_account_no_required", "El campo bal_account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(globalDimension1Code))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.global_dimension_1_code_required", "El campo global_dimension_1_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(globalDimension2Code))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.global_dimension_2_code_required", "El campo global_dimension_2_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(userId))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.user_id_required", "El campo user_id es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.source_code_required", "El campo source_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(businessUnitCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.business_unit_code_required", "El campo business_unit_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalBatchName))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.journal_batch_name_required", "El campo journal_batch_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(reasonCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.reason_code_required", "El campo reason_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(genBusPostingGroup))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.gen_bus_posting_group_required", "El campo gen_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(genProdPostingGroup))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.gen_prod_posting_group_required", "El campo gen_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(externalDocumentNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.external_document_no_required", "El campo external_document_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.source_no_required", "El campo source_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(noSeries))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.no_series_required", "El campo no_series es obligatorio."));
        if (string.IsNullOrWhiteSpace(taxAreaCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.tax_area_code_required", "El campo tax_area_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(taxGroupCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.tax_group_code_required", "El campo tax_group_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(vatBusPostingGroup))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.vat_bus_posting_group_required", "El campo vat_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(vatProdPostingGroup))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.vat_prod_posting_group_required", "El campo vat_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(icPartnerCode))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.ic_partner_code_required", "El campo ic_partner_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(prodOrderNo))
            return OperationResult<GLEntry, DomainError>.Fail(DomainError.Validation("finance.prod_order_no_required", "El campo prod_order_no es obligatorio."));

        var entity = new GLEntry()
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            GLAccountNo = gLAccountNo.Trim(),
            PostingDate = postingDate,
            DocumentType = documentType,
            DocumentNo = documentNo.Trim(),
            Description = description.Trim(),
            BalAccountNo = balAccountNo.Trim(),
            Amount = amount,
            GlobalDimension1Code = globalDimension1Code.Trim(),
            GlobalDimension2Code = globalDimension2Code.Trim(),
            UserId = userId.Trim(),
            SourceCode = sourceCode.Trim(),
            SystemCreatedEntry = systemCreatedEntry,
            PriorYearEntry = priorYearEntry,
            JobNo = jobNo,
            Quantity = quantity,
            VatAmount = vatAmount,
            BusinessUnitCode = businessUnitCode.Trim(),
            JournalBatchName = journalBatchName.Trim(),
            ReasonCode = reasonCode.Trim(),
            GenPostingType = genPostingType,
            GenBusPostingGroup = genBusPostingGroup.Trim(),
            GenProdPostingGroup = genProdPostingGroup.Trim(),
            BalAccountType = balAccountType,
            TransactionNo = transactionNo,
            DebitAmount = debitAmount,
            CreditAmount = creditAmount,
            DocumentDate = documentDate,
            ExternalDocumentNo = externalDocumentNo.Trim(),
            SourceType = sourceType,
            SourceNo = sourceNo.Trim(),
            NoSeries = noSeries.Trim(),
            TaxAreaCode = taxAreaCode.Trim(),
            TaxLiable = taxLiable,
            TaxGroupCode = taxGroupCode.Trim(),
            UseTax = useTax,
            VatBusPostingGroup = vatBusPostingGroup.Trim(),
            VatProdPostingGroup = vatProdPostingGroup.Trim(),
            AdditionalCurrencyAmount = additionalCurrencyAmount,
            AddCurrencyDebitAmount = addCurrencyDebitAmount,
            AddCurrencyCreditAmount = addCurrencyCreditAmount,
            CloseIncomeStatementDimId = closeIncomeStatementDimId,
            IcPartnerCode = icPartnerCode.Trim(),
            Reversed = reversed,
            ReversedByEntryNo = reversedByEntryNo,
            ReversedEntryNo = reversedEntryNo,
            DimensionSetId = dimensionSetId,
            ProdOrderNo = prodOrderNo.Trim(),
            FaEntryType = faEntryType,
            FaEntryNo = faEntryNo,
        };

        return OperationResult<GLEntry, DomainError>.Ok(entity);
    }
}
