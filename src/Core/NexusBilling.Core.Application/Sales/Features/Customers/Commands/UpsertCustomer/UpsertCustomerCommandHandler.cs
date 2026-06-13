using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Customers.Commands.UpsertCustomer;

public sealed class UpsertCustomerCommandHandler(ICustomerRepository repo, NoSeriesService noSeries, IUnitOfWork uow)
    : IRequestHandler<UpsertCustomerCommand, UpsertCustomerResult>
{
    private const string CustomerSeriesCode = "CUST";

    public async Task<UpsertCustomerResult> Handle(UpsertCustomerCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);
        var lookupNo = cmd.ExistingNo ?? cmd.No;
        var existing = lookupNo is not null
            ? await repo.GetByNoForTenantAsync(cmd.TenantId, lookupNo, ct)
            : null;

        if (existing is not null)
        {
            existing.Name = cmd.Name;
            existing.Address = cmd.Address;
            existing.City = cmd.City;
            existing.Contact = cmd.Contact;
            existing.PhoneNo = cmd.PhoneNo;
            existing.Email = cmd.Email;
            existing.CreditLimit = cmd.CreditLimit;
            existing.VatRegistrationNo = cmd.VatRegistrationNo;
            existing.PaymentTermsCode = cmd.PaymentTermsCode;
            existing.PaymentMethodCode = cmd.PaymentMethodCode;
            existing.SalespersonCode = cmd.SalespersonCode;
            existing.CurrencyCode = cmd.CurrencyCode;
            existing.CustomerPostingGroup = cmd.CustomerPostingGroup;
            existing.CountryRegionCode = cmd.CountryRegionCode;
            await repo.UpdateAsync(existing, ct);
            await uow.SaveChangesAsync(ct);
            return new UpsertCustomerResult(false, existing.No);
        }

        var assignedNo = cmd.No is { Length: > 0 } manual
            ? manual
            : await noSeries.GetNextNoAsync(cmd.TenantId, CustomerSeriesCode, ct);

        var result = Domain.Sales.Entities.Customer.Create(
            tid, assignedNo, cmd.Name, cmd.Address, cmd.City, cmd.Contact);

        if (!result.IsSuccess)
            throw new InvalidOperationException(result.GetError()!.Message);

        var c = result.GetValue()!;
        c.PhoneNo = cmd.PhoneNo;
        c.Email = cmd.Email;
        c.CreditLimit = cmd.CreditLimit;
        c.VatRegistrationNo = cmd.VatRegistrationNo;
        c.PaymentTermsCode = cmd.PaymentTermsCode;
        c.PaymentMethodCode = cmd.PaymentMethodCode;
        c.SalespersonCode = cmd.SalespersonCode;
        c.CurrencyCode = cmd.CurrencyCode;
        c.CustomerPostingGroup = cmd.CustomerPostingGroup;
        c.CountryRegionCode = cmd.CountryRegionCode;

        await repo.AddAsync(c, ct);
        await uow.SaveChangesAsync(ct);
        return new UpsertCustomerResult(true, c.No);
    }
}
