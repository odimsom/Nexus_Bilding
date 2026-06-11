using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TransferHeader : Entity
{
    private TransferHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string TransferFromCode { get; private set; }
    public string TransferFromName { get; private set; }
    public string TransferFromName2 { get; private set; }
    public string TransferFromAddress { get; private set; }
    public string TransferFromAddress2 { get; private set; }
    public string TransferFromPostCode { get; private set; }
    public string TransferFromCity { get; private set; }
    public string TransferFromCounty { get; private set; }
    public string TrsfFromCountryRegionCode { get; private set; }
    public string TransferToCode { get; private set; }
    public string TransferToName { get; private set; }
    public string TransferToName2 { get; private set; }
    public string TransferToAddress { get; private set; }
    public string TransferToAddress2 { get; private set; }
    public string TransferToPostCode { get; private set; }
    public string TransferToCity { get; private set; }
    public string TransferToCounty { get; private set; }
    public string TrsfToCountryRegionCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public DateTime? ReceiptDate { get; private set; }
    public short Status { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string InTransitCode { get; private set; }
    public string NoSeries { get; private set; }
    public string LastShipmentNo { get; private set; }
    public string LastReceiptNo { get; private set; }
    public string TransferFromContact { get; private set; }
    public string TransferToContact { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string ShippingTime { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string EntryExitPoint { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public int DimensionSetId { get; private set; }
    public short ShippingAdvice { get; private set; }
    public int PostingFromWhseRef { get; private set; }
    public string OutboundWhseHandlingTime { get; private set; }
    public string InboundWhseHandlingTime { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<TransferHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TransferHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TransferHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<TransferHeader, DomainError>.Ok(entity);
    }
}
