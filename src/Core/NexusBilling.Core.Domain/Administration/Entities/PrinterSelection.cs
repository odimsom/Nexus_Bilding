using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class PrinterSelection : Entity
{
    private PrinterSelection() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public int ReportId { get; private set; }
    public string PrinterName { get; private set; }

    public static OperationResult<PrinterSelection, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PrinterSelection, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PrinterSelection()
        {
            TenantId = tenantId
        };
        return OperationResult<PrinterSelection, DomainError>.Ok(entity);
    }
}
