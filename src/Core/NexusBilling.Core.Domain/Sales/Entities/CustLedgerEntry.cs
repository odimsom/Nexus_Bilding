using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustLedgerEntry : Entity
{
    private CustLedgerEntry() { }

    public TenantIdentifier TenantId { get; set; }
    public int EntryNo { get; set; }
    public string? CustomerNo { get; set; }
    public DateTime? PostingDate { get; set; }
    public short DocumentType { get; set; }
    public string DocumentNo { get; set; }
    public string Description { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal SalesLcy { get; set; }
    public decimal ProfitLcy { get; set; }
    public decimal InvDiscountLcy { get; set; }
    public string? SellToCustomerNo { get; set; }
    public string CustomerPostingGroup { get; set; }
    public string GlobalDimension1Code { get; set; }
    public string GlobalDimension2Code { get; set; }
    public string? SalespersonCode { get; set; }
    public string UserId { get; set; }
    public string SourceCode { get; set; }
    public string OnHold { get; set; }
    public short AppliesToDocType { get; set; }
    public string AppliesToDocNo { get; set; }
    public bool Open { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PmtDiscountDate { get; set; }
    public decimal OriginalPmtDiscPossible { get; set; }
    public decimal PmtDiscGivenLcy { get; set; }
    public bool Positive { get; set; }
    public int ClosedByEntryNo { get; set; }
    public DateTime? ClosedAtDate { get; set; }
    public decimal ClosedByAmount { get; set; }
    public string AppliesToId { get; set; }
    public string JournalBatchName { get; set; }
    public string ReasonCode { get; set; }
    public short BalAccountType { get; set; }
    public string BalAccountNo { get; set; }
    public int TransactionNo { get; set; }
    public decimal ClosedByAmountLcy { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string ExternalDocumentNo { get; set; }
    public bool CalculateInterest { get; set; }
    public bool ClosingInterestCalculated { get; set; }
    public string NoSeries { get; set; }
    public string? ClosedByCurrencyCode { get; set; }
    public decimal ClosedByCurrencyAmount { get; set; }
    public decimal AdjustedCurrencyFactor { get; set; }
    public decimal OriginalCurrencyFactor { get; set; }
    public decimal RemainingPmtDiscPossible { get; set; }
    public DateTime? PmtDiscToleranceDate { get; set; }
    public decimal MaxPaymentTolerance { get; set; }
    public int LastIssuedReminderLevel { get; set; }
    public decimal AcceptedPaymentTolerance { get; set; }
    public bool AcceptedPmtDiscTolerance { get; set; }
    public decimal PmtToleranceLcy { get; set; }
    public decimal AmountToApply { get; set; }
    public string IcPartnerCode { get; set; }
    public bool ApplyingEntry { get; set; }
    public bool Reversed { get; set; }
    public int ReversedByEntryNo { get; set; }
    public int ReversedEntryNo { get; set; }
    public bool Prepayment { get; set; }
    public string PaymentMethodCode { get; set; }
    public string AppliesToExtDocNo { get; set; }
    public string RecipientBankAccount { get; set; }
    public string MessageToRecipient { get; set; }
    public bool ExportedToPaymentFile { get; set; }
    public int DimensionSetId { get; set; }
    public string DirectDebitMandateId { get; set; }

    public static OperationResult<CustLedgerEntry, DomainError> Create(
        TenantIdentifier tenantId,
        int entryNo,
        string documentNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustLedgerEntry, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));
        if (entryNo <= 0)
            return OperationResult<CustLedgerEntry, DomainError>.Fail(DomainError.Validation("sales.entry_no_invalid", "El número de asiento debe ser mayor a cero."));

        var entity = new CustLedgerEntry
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            DocumentNo = documentNo ?? string.Empty,
            Description = string.Empty,
            CustomerPostingGroup = string.Empty,
            GlobalDimension1Code = string.Empty,
            GlobalDimension2Code = string.Empty,
            UserId = string.Empty,
            SourceCode = string.Empty,
            OnHold = string.Empty,
            AppliesToDocNo = string.Empty,
            AppliesToId = string.Empty,
            JournalBatchName = string.Empty,
            ReasonCode = string.Empty,
            BalAccountNo = string.Empty,
            ExternalDocumentNo = string.Empty,
            NoSeries = string.Empty,
            IcPartnerCode = string.Empty,
            PaymentMethodCode = string.Empty,
            AppliesToExtDocNo = string.Empty,
            RecipientBankAccount = string.Empty,
            MessageToRecipient = string.Empty,
            DirectDebitMandateId = string.Empty
        };

        return OperationResult<CustLedgerEntry, DomainError>.Ok(entity);
    }
}
