using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccScheduleLine : Entity
{
    private AccScheduleLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ScheduleName { get; private set; }
    public int LineNo { get; private set; }
    public string RowNo { get; private set; }
    public string Description { get; private set; }
    public string Totaling { get; private set; }
    public short TotalingType { get; private set; }
    public bool NewPage { get; private set; }
    public int Indentation { get; private set; }
    public short Show { get; private set; }
    public string Dimension1Totaling { get; private set; }
    public string Dimension2Totaling { get; private set; }
    public string Dimension3Totaling { get; private set; }
    public string Dimension4Totaling { get; private set; }
    public bool Bold { get; private set; }
    public bool Italic { get; private set; }
    public bool Underline { get; private set; }
    public bool ShowOppositeSign { get; private set; }
    public short RowType { get; private set; }
    public short AmountType { get; private set; }
    public bool DoubleUnderline { get; private set; }
    public string CostCenterTotaling { get; private set; }
    public string CostObjectTotaling { get; private set; }

    public static OperationResult<AccScheduleLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccScheduleLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccScheduleLine()
        {
            TenantId = tenantId
        };
        return OperationResult<AccScheduleLine, DomainError>.Ok(entity);
    }
}
