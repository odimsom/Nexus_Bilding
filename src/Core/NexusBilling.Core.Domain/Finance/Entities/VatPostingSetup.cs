using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatPostingSetup : Entity
{
    private VatPostingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public short VatCalculationType { get; private set; }
    public decimal Vat { get; private set; }
    public short UnrealizedVatType { get; private set; }
    public bool AdjustForPaymentDiscount { get; private set; }
    public string SalesVatAccount { get; private set; }
    public string SalesVatUnrealAccount { get; private set; }
    public string PurchaseVatAccount { get; private set; }
    public string PurchVatUnrealAccount { get; private set; }
    public string ReverseChrgVatAcc { get; private set; }
    public string ReverseChrgVatUnrealAcc { get; private set; }
    public string VatIdentifier { get; private set; }
    public bool EuService { get; private set; }
    public string VatClauseCode { get; private set; }
    public bool CertificateOfSupplyRequired { get; private set; }
    public string TaxCategory { get; private set; }

    public static OperationResult<VatPostingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatPostingSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatPostingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<VatPostingSetup, DomainError>.Ok(entity);
    }
}
