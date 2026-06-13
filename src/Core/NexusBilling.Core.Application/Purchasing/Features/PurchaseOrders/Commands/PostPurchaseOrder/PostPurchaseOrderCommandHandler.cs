using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;
using System.Reflection;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.PostPurchaseOrder;

public sealed class PostPurchaseOrderCommandHandler(
    IPurchaseHeaderRepository headerRepo,
    IPurchaseLineRepository lineRepo,
    IPurchInvHeaderRepository invoiceHeaderRepo,
    IPurchInvLineRepository invoiceLineRepo,
    IItemRepository itemRepo,
    IItemLedgerEntryRepository itemLedgerRepo,
    IUnitOfWork uow)
    : IRequestHandler<PostPurchaseOrderCommand, PostPurchaseOrderResult>
{
    public async Task<PostPurchaseOrderResult> Handle(PostPurchaseOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        // 1. Fetch Order
        var order = await headerRepo.GetByNoAsync(cmd.No, ct)
            ?? throw new InvalidOperationException($"Pedido {cmd.No} no encontrado.");

        var lines = (await lineRepo.GetByDocumentNoAsync(1 /* Order */, cmd.No, ct)).ToList();

        // 2. Create Posted Invoice
        var invoiceNo = "PINV-" + order.No;
        
        var invoiceHeaderResult = PurchInvHeader.Create(tid);
        if (!invoiceHeaderResult.IsSuccess)
            throw new InvalidOperationException("Error creando cabecera de factura de compra.");

        var invoiceHeader = invoiceHeaderResult.GetValue()!;
        SetProperty(invoiceHeader, "No", invoiceNo);
        SetProperty(invoiceHeader, "BuyFromVendorNo", order.BuyFromVendorNo);
        SetProperty(invoiceHeader, "PayToName", order.PayToName);
        SetProperty(invoiceHeader, "PostingDate", DateTime.UtcNow);
        // Assume Amount fields are public or we use reflection for now if they are private
        SetProperty(invoiceHeader, "Amount", order.Amount);
        SetProperty(invoiceHeader, "AmountIncludingVat", order.AmountIncludingVat);

        await invoiceHeaderRepo.AddAsync(invoiceHeader, ct);

        // 3. Process Lines & Inventory
        foreach (var l in lines)
        {
            var invLineResult = PurchInvLine.Create(tid);
            if (!invLineResult.IsSuccess) continue;

            var invLine = invLineResult.GetValue()!;
            SetProperty(invLine, "DocumentNo", invoiceNo);
            SetProperty(invLine, "No", l.No);
            SetProperty(invLine, "Description", l.Description);
            SetProperty(invLine, "Quantity", l.Quantity);
            SetProperty(invLine, "UnitPrice", l.DirectUnitCost);
            SetProperty(invLine, "Amount", l.Amount);
            SetProperty(invLine, "AmountIncludingVat", l.AmountIncludingVat);

            await invoiceLineRepo.AddAsync(invLine, ct);

            // Inventory increment if it's an Item
            if (l.Type == 2 /* Item */ && !string.IsNullOrEmpty(l.No))
            {
                // Create Item Ledger Entry
                int entryNo = new Random().Next(10000, 99999);
                var ileResult = ItemLedgerEntry.Create(tid, entryNo);
                    if (ileResult.IsSuccess)
                    {
                        var ile = ileResult.GetValue()!;
                        SetProperty(ile, "ItemNo", l.No);
                        SetProperty(ile, "PostingDate", DateTime.UtcNow);
                        SetProperty(ile, "EntryType", 1 /* Purchase */);
                        SetProperty(ile, "DocumentNo", invoiceNo);
                        SetProperty(ile, "Quantity", l.Quantity);

                        await itemLedgerRepo.AddAsync(ile, ct);
                    }
            }
        }

        // 4. Update order status or delete. Let's just update status to 'Released' to keep history for now.
        order.Status = "Released";
        await headerRepo.UpdateAsync(order, ct);

        await uow.SaveChangesAsync(ct);
        return new PostPurchaseOrderResult(invoiceNo);
    }

    private void SetProperty(object obj, string propertyName, object value)
    {
        var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, value);
        }
        else
        {
            var field = obj.GetType().GetField($"<{propertyName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
    }
}
