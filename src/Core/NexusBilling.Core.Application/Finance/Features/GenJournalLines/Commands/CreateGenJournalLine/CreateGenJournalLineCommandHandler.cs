using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Finance.Entities;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.CreateGenJournalLine;

internal sealed class CreateGenJournalLineCommandHandler(
    IGenJournalLineRepository repo,
    IUnitOfWork uow)
    : IRequestHandler<CreateGenJournalLineCommand, OperationResult<Guid, DomainError>>
{
    public async Task<OperationResult<Guid, DomainError>> Handle(CreateGenJournalLineCommand request, CancellationToken cancellationToken)
    {
        var existingLines = await repo.GetLinesAsync(request.TenantId, request.JournalTemplateName, request.JournalBatchName, cancellationToken);
        var nextLineNo = existingLines.Any() ? existingLines.Max(x => x.LineNo) + 10000 : 10000;

        var lineResult = GenJournalLine.Create(
            TenantIdentifier.Create(request.TenantId),
            request.JournalTemplateName,
            nextLineNo,
            0, // AccountType: G/L Account
            request.AccountNo,
            request.PostingDate,
            0, // DocumentType: Blank
            request.DocumentNo,
            request.Description,
            0, // Vat
            request.BalAccountNo,
            null, // CurrencyCode
            request.Amount,
            request.Amount > 0 ? request.Amount : 0, // DebitAmount
            request.Amount < 0 ? Math.Abs(request.Amount) : 0, // CreditAmount
            request.Amount, // AmountLcy
            0, // BalanceLcy
            1, // CurrencyFactor
            0, // SalesPurchLcy
            0, // ProfitLcy
            0, // InvDiscountLcy
            "", // BillToPayToNo
            "", // PostingGroup
            "", // ShortcutDimension1Code
            "", // ShortcutDimension2Code
            null, // SalespersPurchCode
            "DIARIO", // SourceCode
            false, // SystemCreatedEntry
            "", // OnHold
            0, // AppliesToDocType
            "", // AppliesToDocNo
            null, // DueDate
            null, // PmtDiscountDate
            0, // PaymentDiscount
            null, // JobNo
            1, // Quantity
            0, // VatAmount
            0, // VatPosting
            "", // PaymentTermsCode
            "", // AppliesToId
            "", // BusinessUnitCode
            request.JournalBatchName,
            "", // ReasonCode
            0, // RecurringMethod
            null, // ExpirationDate
            "", // RecurringFrequency
            0, // GenPostingType
            "", // GenBusPostingGroup
            "", // GenProdPostingGroup
            0, // VatCalculationType
            false, // Eu3PartyTrade
            false, // AllowApplication
            0, // BalAccountType
            0, // BalGenPostingType
            "", // BalGenBusPostingGroup
            "", // BalGenProdPostingGroup
            0, // BalVatCalculationType
            0, // BalVat
            0, // BalVatAmount
            0, // BankPaymentType
            0, // VatBaseAmount
            0, // BalVatBaseAmount
            false, // Correction
            false, // CheckPrinted
            null, // DocumentDate
            "", // ExternalDocumentNo
            0, // SourceType
            "", // SourceNo
            "", // PostingNoSeries
            "", // TaxAreaCode
            false, // TaxLiable
            "", // TaxGroupCode
            false, // UseTax
            "", // BalTaxAreaCode
            false, // BalTaxLiable
            "", // BalTaxGroupCode
            false, // BalUseTax
            "", // VatBusPostingGroup
            "", // VatProdPostingGroup
            "", // BalVatBusPostingGroup
            "", // BalVatProdPostingGroup
            0, // AdditionalCurrencyPosting
            1, // FaAddCurrencyFactor
            null, // SourceCurrencyCode
            0, // SourceCurrencyAmount
            0, // SourceCurrVatBaseAmount
            0, // SourceCurrVatAmount
            0, // VatBaseDiscount
            0, // VatAmountLcy
            0, // VatBaseAmountLcy
            0, // BalVatAmountLcy
            0, // BalVatBaseAmountLcy
            false, // ReversingEntry
            false, // AllowZeroAmountPosting
            "", // ShipToOrderAddressCode
            0, // VatDifference
            0, // BalVatDifference
            "", // IcPartnerCode
            0, // IcDirection
            "", // IcPartnerGLAccNo
            0, // IcPartnerTransactionNo
            "", // SellToBuyFromNo
            "", // VatRegistrationNo
            null, // CountryRegionCode
            false, // Prepayment
            false, // FinancialVoid
            0, // IncomingDocumentEntryNo
            "", // CreditorNo
            "", // PaymentReference
            "", // PaymentMethodCode
            "", // AppliesToExtDocNo
            "", // RecipientBankAccount
            "", // MessageToRecipient
            false, // ExportedToPaymentFile
            0, // DimensionSetId
            "", // CreditCardNo
            "", // JobTaskNo
            0, // JobUnitPriceLcy
            0, // JobTotalPriceLcy
            0, // JobQuantity
            0, // JobUnitCostLcy
            0, // JobLineDiscount
            0, // JobLineDiscAmountLcy
            "", // JobUnitOfMeasureCode
            0, // JobLineType
            0, // JobUnitPrice
            0, // JobTotalPrice
            0, // JobUnitCost
            0, // JobTotalCost
            0, // JobLineDiscountAmount
            0, // JobLineAmount
            0, // JobTotalCostLcy
            0, // JobLineAmountLcy
            1, // JobCurrencyFactor
            "", // JobCurrencyCode
            0, // JobPlanningLineNo
            0, // JobRemainingQty
            "", // DirectDebitMandateId
            0, // DataExchEntryNo
            "", // PayerInformation
            "", // TransactionInformation
            0, // DataExchLineNo
            false, // AppliedAutomatically
            "", // DeferralCode
            0, // DeferralLineNo
            null, // CampaignNo
            "", // ProdOrderNo
            null, // FaPostingDate
            0, // FaPostingType
            "", // DepreciationBookCode
            0, // SalvageValue
            0, // NoOfDepreciationDays
            false, // DeprUntilFaPostingDate
            false, // DeprAcquisitionCost
            null, // MaintenanceCode
            null, // InsuranceNo
            "", // BudgetedFaNo
            "", // DuplicateInDepreciationBook
            false, // UseDuplicationList
            false, // FaReclassificationEntry
            0, // FaErrorEntryNo
            false, // IndexEntry
            0, // SourceLineNo
            "" // Comment
        );

        if (!lineResult.IsSuccess)
            return OperationResult<Guid, DomainError>.Fail(lineResult.GetError()!);

        var line = lineResult.GetValue()!;

        await repo.AddAsync(line);
        await uow.SaveChangesAsync(cancellationToken);

        return OperationResult<Guid, DomainError>.Ok(line.Id);
    }
}
