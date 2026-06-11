using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class PaymentJnlExportErrorText : Entity
{
    private PaymentJnlExportErrorText() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int JournalLineNo { get; private set; }
    public int LineNo { get; private set; }
    public string ErrorText { get; private set; }
    public string DocumentNo { get; private set; }
    public string AdditionalInformation { get; private set; }
    public string SupportUrl { get; private set; }

    public static OperationResult<PaymentJnlExportErrorText, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentJnlExportErrorText, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentJnlExportErrorText()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentJnlExportErrorText, DomainError>.Ok(entity);
    }
}
