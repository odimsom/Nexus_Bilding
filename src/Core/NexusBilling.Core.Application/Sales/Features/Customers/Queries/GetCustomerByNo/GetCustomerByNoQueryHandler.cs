using MediatR;
using NexusBilling.Core.Application.Sales.DTOs;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Customers.Queries.GetCustomerByNo;

public sealed class GetCustomerByNoQueryHandler(ICustomerRepository repo)
    : IRequestHandler<GetCustomerByNoQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByNoQuery request, CancellationToken cancellationToken)
    {
        var c = await repo.GetByNoForTenantAsync(request.TenantId, request.No, cancellationToken);
        if (c is null) return null;
        return new CustomerDto(
            c.No, c.Name, c.Address, c.City, c.Contact, c.Blocked,
            c.PhoneNo, c.Email, c.CreditLimit, c.Balance, c.BalanceDue,
            c.VatRegistrationNo, c.PaymentTermsCode, c.PaymentMethodCode,
            c.SalespersonCode, c.CurrencyCode, c.CustomerPostingGroup, c.CountryRegionCode);
    }
}
