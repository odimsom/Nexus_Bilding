using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Queries;

public record GetVendorsQuery(Guid TenantId, int Page, int PageSize) : IRequest<(IReadOnlyList<VendorDto> Items, int TotalCount)>;

public record VendorDto(
    string No,
    string Name,
    string Address,
    string City,
    string Contact,
    bool Blocked);
