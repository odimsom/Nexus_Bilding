using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderHeader;

public sealed class UpdatePurchaseOrderHeaderCommandHandler(
    IPurchaseHeaderRepository headerRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdatePurchaseOrderHeaderCommand, bool>
{
    public async Task<bool> Handle(UpdatePurchaseOrderHeaderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var header = await headerRepo.GetByNoAsync(cmd.No, ct)
            ?? throw new InvalidOperationException($"Pedido {cmd.No} no encontrado.");

        if (header.TenantId != tid)
            throw new InvalidOperationException($"Pedido {cmd.No} no encontrado.");

        if (header.Status != "Open")
            throw new InvalidOperationException("Solo se pueden editar pedidos abiertos.");

        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        header.DueDate = UtcN(cmd.DueDate);
        header.CurrencyCode = cmd.CurrencyCode;
        header.PaymentTermsCode = cmd.PaymentTermsCode;
        header.ExternalDocumentNo = cmd.ExternalDocumentNo ?? string.Empty;

        await headerRepo.UpdateAsync(header, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
