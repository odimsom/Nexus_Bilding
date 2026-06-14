using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.UpdateSalesOrderHeader;

public sealed class UpdateSalesOrderHeaderCommandHandler(
    ISalesHeaderRepository headerRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdateSalesOrderHeaderCommand, bool>
{
    public async Task<bool> Handle(UpdateSalesOrderHeaderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var headers = await headerRepo.FindAsync(h =>
            h.TenantId == tid && h.No == cmd.No && h.DocumentType == "Order");
        var order = headers.FirstOrDefault()
            ?? throw new InvalidOperationException($"Orden {cmd.No} no encontrada.");

        if (order.Status != "Open")
            throw new InvalidOperationException("Solo se pueden editar órdenes abiertas.");

        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        order.DueDate = UtcN(cmd.DueDate);
        order.CurrencyCode = cmd.CurrencyCode;
        order.PaymentTermsCode = cmd.PaymentTermsCode;
        order.PaymentMethodCode = cmd.PaymentMethodCode;
        order.SalespersonCode = cmd.SalespersonCode;
        order.ExternalDocumentNo = cmd.ExternalDocumentNo ?? string.Empty;

        await headerRepo.UpdateAsync(order, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
