using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class VendorLedgerEntry : Entity
{
    private VendorLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string? VendorNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string? CurrencyCode { get; private set; }
    public decimal PurchaseLcy { get; private set; }
    public decimal InvDiscountLcy { get; private set; }
    public string? BuyFromVendorNo { get; private set; }
    public string VendorPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string? PurchaserCode { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public bool Open { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public decimal OriginalPmtDiscPossible { get; private set; }
    public decimal PmtDiscRcdLcy { get; private set; }
    public bool Positive { get; private set; }
    public int ClosedByEntryNo { get; private set; }
    public DateTime? ClosedAtDate { get; private set; }
    public decimal ClosedByAmount { get; private set; }
    public string AppliesToId { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public int TransactionNo { get; private set; }
    public decimal ClosedByAmountLcy { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string NoSeries { get; private set; }
    public string? ClosedByCurrencyCode { get; private set; }
    public decimal ClosedByCurrencyAmount { get; private set; }
    public decimal AdjustedCurrencyFactor { get; private set; }
    public decimal OriginalCurrencyFactor { get; private set; }
    public decimal RemainingPmtDiscPossible { get; private set; }
    public DateTime? PmtDiscToleranceDate { get; private set; }
    public decimal MaxPaymentTolerance { get; private set; }
    public decimal AcceptedPaymentTolerance { get; private set; }
    public bool AcceptedPmtDiscTolerance { get; private set; }
    public decimal PmtToleranceLcy { get; private set; }
    public decimal AmountToApply { get; private set; }
    public string IcPartnerCode { get; private set; }
    public bool ApplyingEntry { get; private set; }
    public bool Reversed { get; private set; }
    public int ReversedByEntryNo { get; private set; }
    public int ReversedEntryNo { get; private set; }
    public bool Prepayment { get; private set; }
    public string CreditorNo { get; private set; }
    public string PaymentReference { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string AppliesToExtDocNo { get; private set; }
    public string RecipientBankAccount { get; private set; }
    public string MessageToRecipient { get; private set; }
    public bool ExportedToPaymentFile { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<VendorLedgerEntry, DomainError> Create(
        TenantIdentifier tenantId,
        int entryNo,
        string? vendorNo,
        DateTime? postingDate,
        short documentType,
        string documentNo,
        string description,
        string? currencyCode,
        decimal purchaseLcy,
        decimal invDiscountLcy,
        string? buyFromVendorNo,
        string vendorPostingGroup,
        string globalDimension1Code,
        string globalDimension2Code,
        string? purchaserCode,
        string userId,
        string sourceCode,
        string onHold,
        short appliesToDocType,
        string appliesToDocNo,
        bool open,
        DateTime? dueDate,
        DateTime? pmtDiscountDate,
        decimal originalPmtDiscPossible,
        decimal pmtDiscRcdLcy,
        bool positive,
        int closedByEntryNo,
        DateTime? closedAtDate,
        decimal closedByAmount,
        string appliesToId,
        string journalBatchName,
        string reasonCode,
        short balAccountType,
        string balAccountNo,
        int transactionNo,
        decimal closedByAmountLcy,
        DateTime? documentDate,
        string externalDocumentNo,
        string noSeries,
        string? closedByCurrencyCode,
        decimal closedByCurrencyAmount,
        decimal adjustedCurrencyFactor,
        decimal originalCurrencyFactor,
        decimal remainingPmtDiscPossible,
        DateTime? pmtDiscToleranceDate,
        decimal maxPaymentTolerance,
        decimal acceptedPaymentTolerance,
        bool acceptedPmtDiscTolerance,
        decimal pmtToleranceLcy,
        decimal amountToApply,
        string icPartnerCode,
        bool applyingEntry,
        bool reversed,
        int reversedByEntryNo,
        int reversedEntryNo,
        bool prepayment,
        string creditorNo,
        string paymentReference,
        string paymentMethodCode,
        string appliesToExtDocNo,
        string recipientBankAccount,
        string messageToRecipient,
        bool exportedToPaymentFile,
        int dimensionSetId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VendorLedgerEntry, DomainError>.Fail(DomainError.Validation("purchasing.vendor_ledger_entry.tenant_id_required", "TenantId is required."));
        if (entryNo <= 0)
            return OperationResult<VendorLedgerEntry, DomainError>.Fail(DomainError.Validation("purchasing.vendor_ledger_entry.entry_no_invalid", "EntryNo must be greater than zero."));

        var entity = new VendorLedgerEntry
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            VendorNo = vendorNo,
            PostingDate = postingDate,
            DocumentType = documentType,
            DocumentNo = documentNo ?? string.Empty,
            Description = description ?? string.Empty,
            CurrencyCode = currencyCode,
            PurchaseLcy = purchaseLcy,
            InvDiscountLcy = invDiscountLcy,
            BuyFromVendorNo = buyFromVendorNo,
            VendorPostingGroup = vendorPostingGroup ?? string.Empty,
            GlobalDimension1Code = globalDimension1Code ?? string.Empty,
            GlobalDimension2Code = globalDimension2Code ?? string.Empty,
            PurchaserCode = purchaserCode,
            UserId = userId ?? string.Empty,
            SourceCode = sourceCode ?? string.Empty,
            OnHold = onHold ?? string.Empty,
            AppliesToDocType = appliesToDocType,
            AppliesToDocNo = appliesToDocNo ?? string.Empty,
            Open = open,
            DueDate = dueDate,
            PmtDiscountDate = pmtDiscountDate,
            OriginalPmtDiscPossible = originalPmtDiscPossible,
            PmtDiscRcdLcy = pmtDiscRcdLcy,
            Positive = positive,
            ClosedByEntryNo = closedByEntryNo,
            ClosedAtDate = closedAtDate,
            ClosedByAmount = closedByAmount,
            AppliesToId = appliesToId ?? string.Empty,
            JournalBatchName = journalBatchName ?? string.Empty,
            ReasonCode = reasonCode ?? string.Empty,
            BalAccountType = balAccountType,
            BalAccountNo = balAccountNo ?? string.Empty,
            TransactionNo = transactionNo,
            ClosedByAmountLcy = closedByAmountLcy,
            DocumentDate = documentDate,
            ExternalDocumentNo = externalDocumentNo ?? string.Empty,
            NoSeries = noSeries ?? string.Empty,
            ClosedByCurrencyCode = closedByCurrencyCode,
            ClosedByCurrencyAmount = closedByCurrencyAmount,
            AdjustedCurrencyFactor = adjustedCurrencyFactor,
            OriginalCurrencyFactor = originalCurrencyFactor,
            RemainingPmtDiscPossible = remainingPmtDiscPossible,
            PmtDiscToleranceDate = pmtDiscToleranceDate,
            MaxPaymentTolerance = maxPaymentTolerance,
            AcceptedPaymentTolerance = acceptedPaymentTolerance,
            AcceptedPmtDiscTolerance = acceptedPmtDiscTolerance,
            PmtToleranceLcy = pmtToleranceLcy,
            AmountToApply = amountToApply,
            IcPartnerCode = icPartnerCode ?? string.Empty,
            ApplyingEntry = applyingEntry,
            Reversed = reversed,
            ReversedByEntryNo = reversedByEntryNo,
            ReversedEntryNo = reversedEntryNo,
            Prepayment = prepayment,
            CreditorNo = creditorNo ?? string.Empty,
            PaymentReference = paymentReference ?? string.Empty,
            PaymentMethodCode = paymentMethodCode ?? string.Empty,
            AppliesToExtDocNo = appliesToExtDocNo ?? string.Empty,
            RecipientBankAccount = recipientBankAccount ?? string.Empty,
            MessageToRecipient = messageToRecipient ?? string.Empty,
            ExportedToPaymentFile = exportedToPaymentFile,
            DimensionSetId = dimensionSetId
        };

        return OperationResult<VendorLedgerEntry, DomainError>.Ok(entity);
    }
}
