using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankExportImportSetup : Entity
{
    private BankExportImportSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public short Direction { get; private set; }
    public int ProcessingCodeunitId { get; private set; }
    public int ProcessingXmlportId { get; private set; }
    public string DataExchDefCode { get; private set; }
    public bool PreserveNonLatinCharacters { get; private set; }
    public int CheckExportCodeunit { get; private set; }

    public static OperationResult<BankExportImportSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankExportImportSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankExportImportSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<BankExportImportSetup, DomainError>.Ok(entity);
    }
}
