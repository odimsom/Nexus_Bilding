using MediatR;
using NexusBilling.Core.Application.Sales.DTOs;

namespace NexusBilling.Core.Application.Sales.Queries;

public record GetCustomerByNoQuery(Guid TenantId, string No) : IRequest<CustomerDto?>;
