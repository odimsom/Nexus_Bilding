using MediatR;
using NexusBilling.Core.Application.Sales.DTOs;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Queries;

public sealed class GetCustomersQueryHandler(ICustomerRepository repo)
    : IRequestHandler<GetCustomersQuery, GetCustomersResult>
{
    public async Task<GetCustomersResult> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var ps = Math.Clamp(request.PageSize, 1, 200);
        var pg = Math.Max(1, request.Page);

        var (items, total) = await repo.ListAsync(
            request.TenantId, request.Search, request.Blocked, pg, ps, cancellationToken);

        var totalPages = (int)Math.Ceiling(total / (double)ps);

        var dtos = items.Select(c => new CustomerListDto(
            c.No, c.Name, c.City, c.Contact, c.Blocked,
            c.SalespersonCode, c.PaymentTermsCode, c.Balance, c.BalanceDue))
            .ToList();

        return new GetCustomersResult(dtos, total, totalPages);
    }
}
