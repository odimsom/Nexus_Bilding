using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GenJournalBatch : Entity
{
    private GenJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public bool CopyVatSetupToJnlLines { get; private set; }
    public bool AllowVatDifference { get; private set; }
    public bool AllowPaymentExport { get; private set; }
    public string BankStatementImportFormat { get; private set; }
    public bool SuggestBalancingAmount { get; private set; }

    public static OperationResult<GenJournalBatch, DomainError> Create(
        TenantIdentifier tenantId,
        string journalTemplateName,
        string name,
        string description,
        string reasonCode,
        short balAccountType,
        string balAccountNo,
        string noSeries,
        string postingNoSeries,
        bool copyVatSetupToJnlLines,
        bool allowVatDifference,
        bool allowPaymentExport,
        string bankStatementImportFormat,
        bool suggestBalancingAmount)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(journalTemplateName))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.journal_template_name_required", "El campo journal_template_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(reasonCode))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.reason_code_required", "El campo reason_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(balAccountNo))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.bal_account_no_required", "El campo bal_account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(noSeries))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.no_series_required", "El campo no_series es obligatorio."));
        if (string.IsNullOrWhiteSpace(postingNoSeries))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.posting_no_series_required", "El campo posting_no_series es obligatorio."));
        if (string.IsNullOrWhiteSpace(bankStatementImportFormat))
            return OperationResult<GenJournalBatch, DomainError>.Fail(DomainError.Validation("finance.bank_statement_import_format_required", "El campo bank_statement_import_format es obligatorio."));

        var entity = new GenJournalBatch()
        {
            TenantId = tenantId,
            JournalTemplateName = journalTemplateName.Trim(),
            Name = name.Trim(),
            Description = description.Trim(),
            ReasonCode = reasonCode.Trim(),
            BalAccountType = balAccountType,
            BalAccountNo = balAccountNo.Trim(),
            NoSeries = noSeries.Trim(),
            PostingNoSeries = postingNoSeries.Trim(),
            CopyVatSetupToJnlLines = copyVatSetupToJnlLines,
            AllowVatDifference = allowVatDifference,
            AllowPaymentExport = allowPaymentExport,
            BankStatementImportFormat = bankStatementImportFormat.Trim(),
            SuggestBalancingAmount = suggestBalancingAmount,
        };

        return OperationResult<GenJournalBatch, DomainError>.Ok(entity);
    }
}
