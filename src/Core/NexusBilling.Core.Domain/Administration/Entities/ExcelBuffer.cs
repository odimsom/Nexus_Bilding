using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ExcelBuffer : Entity
{
    private ExcelBuffer() { }

    public TenantIdentifier TenantId { get; private set; }
    public int RowNo { get; private set; }
    public string Xlrowid { get; private set; }
    public int ColumnNo { get; private set; }
    public string Xlcolid { get; private set; }
    public string CellValueAsText { get; private set; }
    public string Comment { get; private set; }
    public string Formula { get; private set; }
    public bool Bold { get; private set; }
    public bool Italic { get; private set; }
    public bool Underline { get; private set; }
    public string Numberformat { get; private set; }
    public string Formula2 { get; private set; }
    public string Formula3 { get; private set; }
    public string Formula4 { get; private set; }
    public short CellType { get; private set; }
    public bool DoubleUnderline { get; private set; }

    public static OperationResult<ExcelBuffer, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ExcelBuffer, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ExcelBuffer()
        {
            TenantId = tenantId
        };
        return OperationResult<ExcelBuffer, DomainError>.Ok(entity);
    }
}
