using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceContractLine : Entity
{
    private ServiceContractLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ContractType { get; private set; }
    public string ContractNo { get; private set; }
    public int LineNo { get; private set; }
    public short ContractStatus { get; private set; }
    public string ServiceItemNo { get; private set; }
    public string Description { get; private set; }
    public string SerialNo { get; private set; }
    public string ServiceItemGroupCode { get; private set; }
    public string CustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string ItemNo { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal ResponseTimeHours { get; private set; }
    public DateTime? LastPlannedServiceDate { get; private set; }
    public DateTime? NextPlannedServiceDate { get; private set; }
    public DateTime? LastServiceDate { get; private set; }
    public DateTime? LastPreventiveMaintDate { get; private set; }
    public DateTime? InvoicedToDate { get; private set; }
    public DateTime? CreditMemoDate { get; private set; }
    public DateTime? ContractExpirationDate { get; private set; }
    public string ServicePeriod { get; private set; }
    public decimal LineValue { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineAmount { get; private set; }
    public string VariantCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public bool NewLine { get; private set; }
    public bool Credited { get; private set; }
    public decimal LineCost { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal Profit { get; private set; }

    public static OperationResult<ServiceContractLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceContractLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceContractLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceContractLine, DomainError>.Ok(entity);
    }
}
