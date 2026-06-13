using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using System.Reflection;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.PostPurchaseOrder;

public sealed class PostPurchaseOrderCommandHandler(
    IPurchaseHeaderRepository headerRepo,
    IPurchaseLineRepository lineRepo,
    IPurchInvHeaderRepository invoiceHeaderRepo,
    IPurchInvLineRepository invoiceLineRepo,
    IItemLedgerEntryRepository itemLedgerRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<PostPurchaseOrderCommand, PostPurchaseOrderResult>
{
    public async Task<PostPurchaseOrderResult> Handle(PostPurchaseOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var order = await headerRepo.GetByNoAsync(cmd.No, ct)
            ?? throw new InvalidOperationException($"Pedido {cmd.No} no encontrado.");

        if (order.TenantId != tid)
            throw new InvalidOperationException($"Pedido {cmd.No} no encontrado.");

        if (order.Status != "Open")
            throw new InvalidOperationException($"El pedido {cmd.No} debe estar en estado Abierto para ser contabilizado.");

        var lines = (await lineRepo.GetByDocumentNoAsync(1, cmd.No, ct)).ToList();
        if (lines.Count == 0)
            throw new InvalidOperationException("El pedido no tiene líneas. Agrega al menos una línea antes de contabilizar.");

        // Generate invoice number via No-Series
        var invoiceNo = await noSeriesService.GetNextNoAsync(cmd.TenantId, "FCP", ct);

        // Create posted invoice header
        var headerResult = PurchInvHeader.Create(tid);
        if (!headerResult.IsSuccess)
            throw new InvalidOperationException("Error al crear cabecera de factura de compra.");

        var invoiceHeader = headerResult.GetValue()!;
        Set(invoiceHeader, "No", invoiceNo);
        Set(invoiceHeader, "BuyFromVendorNo", order.BuyFromVendorNo ?? string.Empty);
        Set(invoiceHeader, "PayToName", order.PayToName ?? string.Empty);
        Set(invoiceHeader, "PostingDate", DateTime.UtcNow);
        Set(invoiceHeader, "OrderNo", order.No);
        Set(invoiceHeader, "CurrencyCode", order.CurrencyCode ?? string.Empty);
        Set(invoiceHeader, "PaymentTermsCode", order.PaymentTermsCode ?? string.Empty);
        Set(invoiceHeader, "VendorInvoiceNo", order.ExternalDocumentNo ?? string.Empty);

        await invoiceHeaderRepo.AddAsync(invoiceHeader, ct);

        // Create invoice lines
        int lineNo = 10000;
        foreach (var l in lines)
        {
            var lineResult = PurchInvLine.Create(tid);
            if (!lineResult.IsSuccess) continue;

            var invLine = lineResult.GetValue()!;
            Set(invLine, "DocumentNo", invoiceNo);
            Set(invLine, "BuyFromVendorNo", order.BuyFromVendorNo ?? string.Empty);
            Set(invLine, "LineNo", lineNo);
            Set(invLine, "No", l.No ?? string.Empty);
            Set(invLine, "Description", l.Description ?? string.Empty);
            Set(invLine, "Quantity", l.Quantity);
            Set(invLine, "DirectUnitCost", l.DirectUnitCost);
            Set(invLine, "Vat", l.Vat);
            Set(invLine, "Amount", l.Amount);
            Set(invLine, "AmountIncludingVat", l.AmountIncludingVat);
            Set(invLine, "UnitOfMeasureCode", l.UnitOfMeasure ?? string.Empty);

            await invoiceLineRepo.AddAsync(invLine, ct);

            // Update inventory for item type lines
            if (l.Type == 2 && !string.IsNullOrEmpty(l.No))
            {
                var entryNo = Math.Abs(Guid.NewGuid().GetHashCode()) % 900000 + 100000;
                var ileResult = ItemLedgerEntry.Create(tid, entryNo);
                if (ileResult.IsSuccess)
                {
                    var ile = ileResult.GetValue()!;
                    Set(ile, "ItemNo", l.No);
                    Set(ile, "PostingDate", DateTime.UtcNow);
                    Set(ile, "EntryType", 1); // Purchase
                    Set(ile, "DocumentNo", invoiceNo);
                    Set(ile, "Quantity", l.Quantity);
                    await itemLedgerRepo.AddAsync(ile, ct);
                }
            }

            lineNo += 10000;
        }

        // Mark order as posted
        order.Status = "Posted";
        await headerRepo.UpdateAsync(order, ct);

        await uow.SaveChangesAsync(ct);
        return new PostPurchaseOrderResult(invoiceNo);
    }

    private static void Set(object obj, string prop, object value)
    {
        var pi = obj.GetType().GetProperty(prop, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (pi?.CanWrite == true) { pi.SetValue(obj, value); return; }
        var fi = obj.GetType().GetField($"<{prop}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
        fi?.SetValue(obj, value);
    }
}
