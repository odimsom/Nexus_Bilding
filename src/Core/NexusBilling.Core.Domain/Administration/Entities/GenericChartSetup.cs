using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class GenericChartSetup : Entity
{
    private GenericChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string IdNav { get; private set; }
    public short SourceType { get; private set; }
    public string Name { get; private set; }
    public string Title { get; private set; }
    public string FilterText { get; private set; }
    public short Type { get; private set; }
    public int SourceId { get; private set; }
    public string ObjectName { get; private set; }
    public int XAxisFieldId { get; private set; }
    public string XAxisFieldName { get; private set; }
    public string XAxisFieldCaption { get; private set; }
    public string XAxisTitle { get; private set; }
    public bool XAxisShowTitle { get; private set; }
    public string YAxisTitle { get; private set; }
    public bool YAxisShowTitle { get; private set; }
    public int ZAxisFieldId { get; private set; }
    public string ZAxisFieldName { get; private set; }
    public string ZAxisFieldCaption { get; private set; }
    public string ZAxisTitle { get; private set; }
    public bool ZAxisShowTitle { get; private set; }

    public static OperationResult<GenericChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenericChartSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new GenericChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<GenericChartSetup, DomainError>.Ok(entity);
    }
}
