using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Administration.Features.ServiceOrders.Commands.InvoiceServiceOrder;

public sealed class InvoiceServiceOrderCommandHandler(
    IServiceHeaderRepository headerRepo,
    IServiceLineRepository lineRepo,
    ISalesInvoiceHeaderRepository invoiceHeaderRepo,
    ISalesInvoiceLineRepository invoiceLineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<InvoiceServiceOrderCommand, string>
{
    public async Task<string> Handle(InvoiceServiceOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var order = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct)
            ?? throw new InvalidOperationException($"Orden de servicio {cmd.No} no encontrada.");

        if (order.Status != 2)
            throw new InvalidOperationException("Solo se pueden facturar órdenes en estado Terminado.");

        var lines = (await lineRepo.GetByDocumentNoAsync(order.DocumentType, cmd.No, ct)).ToList();
        if (lines.Count == 0)
            throw new InvalidOperationException("La orden de servicio no tiene líneas de detalle.");

        var invoiceNo = await noSeriesService.GetNextNoAsync(cmd.TenantId, "FS", ct);

        var headerResult = SalesInvoiceHeader.Create(tid);
        if (!headerResult.IsSuccess)
            throw new InvalidOperationException("Error creando cabecera de factura.");

        var invoiceHeader = headerResult.GetValue()!;
        invoiceHeader.No = invoiceNo;
        invoiceHeader.SellToCustomerNo = order.CustomerNo ?? string.Empty;
        invoiceHeader.SellToCustomerName = order.Name ?? string.Empty;
        invoiceHeader.BillToName = order.BillToName ?? order.Name ?? string.Empty;
        invoiceHeader.PostingDate = DateTime.UtcNow;
        invoiceHeader.DueDate = order.DueDate;
        invoiceHeader.CurrencyCode = order.CurrencyCode ?? string.Empty;
        invoiceHeader.PaymentTermsCode = order.PaymentTermsCode ?? string.Empty;
        invoiceHeader.PaymentMethodCode = order.PaymentMethodCode ?? string.Empty;
        invoiceHeader.SalespersonCode = order.SalespersonCode ?? string.Empty;
        invoiceHeader.OrderNo = order.No;
        invoiceHeader.Amount = lines.Sum(l => l.Amount);
        invoiceHeader.AmountIncludingVat = lines.Sum(l => l.AmountIncludingVat);

        await invoiceHeaderRepo.AddAsync(invoiceHeader, ct);

        int lineNo = 10000;
        foreach (var l in lines)
        {
            var lineResult = SalesInvoiceLine.Create(tid);
            if (!lineResult.IsSuccess) continue;

            var invLine = lineResult.GetValue()!;
            invLine.DocumentNo = invoiceNo;
            invLine.LineNo = lineNo;
            invLine.No = l.No ?? string.Empty;
            invLine.Description = l.Description ?? string.Empty;
            invLine.Quantity = l.Quantity;
            invLine.UnitPrice = l.UnitPrice;
            invLine.LineDiscount = l.LineDiscount;
            invLine.Amount = l.Amount;
            invLine.AmountIncludingVat = l.AmountIncludingVat;
            invLine.UnitOfMeasureCode = l.UnitOfMeasure ?? string.Empty;
            invLine.Vat = l.Vat;
            lineNo += 10000;

            await invoiceLineRepo.AddAsync(invLine, ct);
        }

        order.Status = 3;
        await headerRepo.UpdateAsync(order, ct);

        await uow.SaveChangesAsync(ct);
        return invoiceNo;
    }
}
