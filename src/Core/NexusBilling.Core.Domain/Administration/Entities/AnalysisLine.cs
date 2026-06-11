using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisLine : Entity
{
    private AnalysisLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string AnalysisLineTemplateName { get; private set; }
    public int LineNo { get; private set; }
    public string RowRefNo { get; private set; }
    public string Description { get; private set; }
    public short Type { get; private set; }
    public string Range { get; private set; }
    public bool NewPage { get; private set; }
    public short Show { get; private set; }
    public bool Bold { get; private set; }
    public bool Italic { get; private set; }
    public bool Underline { get; private set; }
    public bool ShowOppositeSign { get; private set; }
    public string Dimension1Totaling { get; private set; }
    public string Dimension2Totaling { get; private set; }
    public string Dimension3Totaling { get; private set; }
    public string GroupDimensionCode { get; private set; }
    public int Indentation { get; private set; }

    public static OperationResult<AnalysisLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisLine()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisLine, DomainError>.Ok(entity);
    }
}
