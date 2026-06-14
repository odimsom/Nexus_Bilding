using MediatR;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Commands.InvoiceServiceOrder;

public record InvoiceServiceOrderCommand(Guid TenantId, string No) : IRequest<string>;
