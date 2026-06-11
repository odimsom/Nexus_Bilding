using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class VendorBankAccount : Entity
{
    private VendorBankAccount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string VendorNo { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string PostCode { get; private set; }
    public string Contact { get; private set; }
    public string PhoneNo { get; private set; }
    public string TelexNo { get; private set; }
    public string BankBranchNo { get; private set; }
    public string BankAccountNo { get; private set; }
    public string TransitNo { get; private set; }
    public string CurrencyCode { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string County { get; private set; }
    public string FaxNo { get; private set; }
    public string TelexAnswerBack { get; private set; }
    public string LanguageCode { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }
    public string Iban { get; private set; }
    public string SwiftCode { get; private set; }
    public string BankClearingCode { get; private set; }
    public string BankClearingStandard { get; private set; }

    public static OperationResult<VendorBankAccount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VendorBankAccount, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VendorBankAccount()
        {
            TenantId = tenantId
        };
        return OperationResult<VendorBankAccount, DomainError>.Ok(entity);
    }
}
