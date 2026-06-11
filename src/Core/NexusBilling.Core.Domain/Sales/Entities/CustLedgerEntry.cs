using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustLedgerEntry : Entity
{
    private CustLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string? CustomerNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string? CurrencyCode { get; private set; }
    public decimal SalesLcy { get; private set; }
    public decimal ProfitLcy { get; private set; }
    public decimal InvDiscountLcy { get; private set; }
    public string? SellToCustomerNo { get; private set; }
    public string CustomerPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string? SalespersonCode { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public string OnHold { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public bool Open { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public decimal OriginalPmtDiscPossible { get; private set; }
    public decimal PmtDiscGivenLcy { get; private set; }
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
    public bool CalculateInterest { get; private set; }
    public bool ClosingInterestCalculated { get; private set; }
    public string NoSeries { get; private set; }
    public string? ClosedByCurrencyCode { get; private set; }
    public decimal ClosedByCurrencyAmount { get; private set; }
    public decimal AdjustedCurrencyFactor { get; private set; }
    public decimal OriginalCurrencyFactor { get; private set; }
    public decimal RemainingPmtDiscPossible { get; private set; }
    public DateTime? PmtDiscToleranceDate { get; private set; }
    public decimal MaxPaymentTolerance { get; private set; }
    public int LastIssuedReminderLevel { get; private set; }
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
    public string PaymentMethodCode { get; private set; }
    public string AppliesToExtDocNo { get; private set; }
    public string RecipientBankAccount { get; private set; }
    public string MessageToRecipient { get; private set; }
    public bool ExportedToPaymentFile { get; private set; }
    public int DimensionSetId { get; private set; }
    public string DirectDebitMandateId { get; private set; }

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
