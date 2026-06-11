using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class InteractionTemplateSetup : Entity
{
    private InteractionTemplateSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string SalesInvoices { get; private set; }
    public string SalesCrMemo { get; private set; }
    public string SalesOrdCnfrmn { get; private set; }
    public string SalesQuotes { get; private set; }
    public string PurchInvoices { get; private set; }
    public string PurchCrMemos { get; private set; }
    public string PurchOrders { get; private set; }
    public string PurchQuotes { get; private set; }
    public string EMails { get; private set; }
    public string CoverSheets { get; private set; }
    public string OutgCalls { get; private set; }
    public string SalesBlnktOrd { get; private set; }
    public string ServOrdPost { get; private set; }
    public string SalesShptNote { get; private set; }
    public string SalesStatement { get; private set; }
    public string SalesRmdr { get; private set; }
    public string ServOrdCreate { get; private set; }
    public string PurchBlnktOrd { get; private set; }
    public string PurchRcpt { get; private set; }
    public string SalesReturnOrder { get; private set; }
    public string SalesReturnReceipt { get; private set; }
    public string SalesFinanceChargeMemo { get; private set; }
    public string PurchReturnShipment { get; private set; }
    public string PurchReturnOrdCnfrmn { get; private set; }
    public string ServiceContract { get; private set; }
    public string ServiceContractQuote { get; private set; }
    public string ServiceQuote { get; private set; }
    public string MeetingInvitation { get; private set; }

    public static OperationResult<InteractionTemplateSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InteractionTemplateSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InteractionTemplateSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<InteractionTemplateSetup, DomainError>.Ok(entity);
    }
}
