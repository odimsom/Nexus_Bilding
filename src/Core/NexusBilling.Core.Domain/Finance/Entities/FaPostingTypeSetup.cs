using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaPostingTypeSetup : Entity
{
    private FaPostingTypeSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public short FaPostingType { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public bool PartOfBookValue { get; private set; }
    public bool PartOfDepreciableBasis { get; private set; }
    public bool IncludeInDeprCalculation { get; private set; }
    public bool IncludeInGainLossCalc { get; private set; }
    public bool ReverseBeforeDisposal { get; private set; }
    public short Sign { get; private set; }
    public bool DepreciationType { get; private set; }
    public bool AcquisitionType { get; private set; }

    public static OperationResult<FaPostingTypeSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaPostingTypeSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaPostingTypeSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<FaPostingTypeSetup, DomainError>.Ok(entity);
    }
}
