using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GenJournalTemplate : Entity
{
    private GenJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int TestReportId { get; private set; }
    public int PageId { get; private set; }
    public int PostingReportId { get; private set; }
    public bool ForcePostingReport { get; private set; }
    public short Type { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public bool Recurring { get; private set; }
    public bool ForceDocBalance { get; private set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public bool CopyVatSetupToJnlLines { get; private set; }
    public bool AllowVatDifference { get; private set; }
    public int CustReceiptReportId { get; private set; }
    public int VendorReceiptReportId { get; private set; }

    public static OperationResult<GenJournalTemplate, DomainError> Create(
        TenantIdentifier tenantId,
        string name,
        string description,
        int testReportId,
        int pageId,
        int postingReportId,
        bool forcePostingReport,
        short type,
        string sourceCode,
        string reasonCode,
        bool recurring,
        bool forceDocBalance,
        short balAccountType,
        string balAccountNo,
        string noSeries,
        string postingNoSeries,
        bool copyVatSetupToJnlLines,
        bool allowVatDifference,
        int custReceiptReportId,
        int vendorReceiptReportId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(sourceCode))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.source_code_required", "El campo source_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(reasonCode))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.reason_code_required", "El campo reason_code es obligatorio."));
        if (string.IsNullOrWhiteSpace(balAccountNo))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.bal_account_no_required", "El campo bal_account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(noSeries))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.no_series_required", "El campo no_series es obligatorio."));
        if (string.IsNullOrWhiteSpace(postingNoSeries))
            return OperationResult<GenJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.posting_no_series_required", "El campo posting_no_series es obligatorio."));

        var entity = new GenJournalTemplate()
        {
            TenantId = tenantId,
            Name = name.Trim(),
            Description = description.Trim(),
            TestReportId = testReportId,
            PageId = pageId,
            PostingReportId = postingReportId,
            ForcePostingReport = forcePostingReport,
            Type = type,
            SourceCode = sourceCode.Trim(),
            ReasonCode = reasonCode.Trim(),
            Recurring = recurring,
            ForceDocBalance = forceDocBalance,
            BalAccountType = balAccountType,
            BalAccountNo = balAccountNo.Trim(),
            NoSeries = noSeries.Trim(),
            PostingNoSeries = postingNoSeries.Trim(),
            CopyVatSetupToJnlLines = copyVatSetupToJnlLines,
            AllowVatDifference = allowVatDifference,
            CustReceiptReportId = custReceiptReportId,
            VendorReceiptReportId = vendorReceiptReportId,
        };

        return OperationResult<GenJournalTemplate, DomainError>.Ok(entity);
    }
}
