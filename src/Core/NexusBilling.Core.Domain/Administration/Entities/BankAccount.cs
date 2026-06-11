using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankAccount : Entity
{
    private BankAccount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string SearchName { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string Contact { get; private set; }
    public string PhoneNo { get; private set; }
    public string TelexNo { get; private set; }
    public string BankAccountNo { get; private set; }
    public string TransitNo { get; private set; }
    public string TerritoryCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string ChainName { get; private set; }
    public decimal MinBalance { get; private set; }
    public string BankAccPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public string LanguageCode { get; private set; }
    public int StatisticsGroup { get; private set; }
    public string OurContactCode { get; private set; }
    public string CountryRegionCode { get; private set; }
    public decimal Amount { get; private set; }
    public bool Blocked { get; private set; }
    public string LastStatementNo { get; private set; }
    public string LastPaymentStatementNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string FaxNo { get; private set; }
    public string TelexAnswerBack { get; private set; }
    public byte[]? Picture { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string LastCheckNo { get; private set; }
    public decimal BalanceLastStatement { get; private set; }
    public string BankBranchNo { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }
    public string NoSeries { get; private set; }
    public int CheckReportId { get; private set; }
    public string Iban { get; private set; }
    public string SwiftCode { get; private set; }
    public string BankStatementImportFormat { get; private set; }
    public string CreditTransferMsgNos { get; private set; }
    public string DirectDebitMsgNos { get; private set; }
    public string SepaDirectDebitExpFormat { get; private set; }
    public string BankStmtServiceRecordId { get; private set; }
    public int TransactionImportTimespan { get; private set; }
    public bool AutomaticStmtImportEnabled { get; private set; }
    public Guid Image { get; private set; }
    public string CreditorNo { get; private set; }
    public string PaymentExportFormat { get; private set; }
    public string BankClearingCode { get; private set; }
    public string BankClearingStandard { get; private set; }
    public string BankNameDataConversion { get; private set; }
    public short MatchToleranceType { get; private set; }
    public decimal MatchToleranceValue { get; private set; }
    public string PositivePayExportCode { get; private set; }

    public static OperationResult<BankAccount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankAccount, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankAccount()
        {
            TenantId = tenantId
        };
        return OperationResult<BankAccount, DomainError>.Ok(entity);
    }
}
