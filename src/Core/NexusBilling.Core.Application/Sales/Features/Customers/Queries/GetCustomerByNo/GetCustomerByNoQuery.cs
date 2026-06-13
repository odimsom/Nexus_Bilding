using MediatR;
using NexusBilling.Core.Application.Sales.DTOs;

namespace NexusBilling.Core.Application.Sales.Features.Customers.Queries.GetCustomerByNo;

public record GetCustomerByNoQuery(Guid TenantId, string No) : IRequest<CustomerDto?>;
