using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.UpdateSalesOrderLines;

public sealed class UpdateSalesOrderLinesCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdateSalesOrderLinesCommand, UpdateSalesOrderLinesResult>
{
    private const decimal VatRate = 0.18m;

    public async Task<UpdateSalesOrderLinesResult> Handle(UpdateSalesOrderLinesCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var headers = await headerRepo.FindAsync(h =>
            h.TenantId == tid && h.No == cmd.OrderNo && h.Status == "Open");
        var header = headers.FirstOrDefault()
            ?? throw new InvalidOperationException($"La orden {cmd.OrderNo} no existe o no está en estado Abierta.");

        // Remove existing lines
        var existingLines = await lineRepo.GetByDocumentNoAsync(1, cmd.OrderNo, ct);
        foreach (var line in existingLines)
            await lineRepo.DeleteAsync(line, ct);

        // Create new lines and compute totals
        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        foreach (var l in cmd.Lines)
        {
            var lineResult = SalesLine.Create(tid, 1, cmd.OrderNo, lineNo);
            if (!lineResult.IsSuccess) continue;

            var line = lineResult.GetValue()!;
            line.SellToCustomerNo = header.SellToCustomerNo;
            line.Type = l.LineType switch { "G/L Account" => 1, "Service" => 3, _ => 2 };
            line.No = l.ItemNo ?? string.Empty;
            line.Description = l.Description;
            line.UnitOfMeasure = l.UnitOfMeasure;
            line.Quantity = l.Quantity;
            line.UnitPrice = l.UnitPrice;
            line.Vat = VatRate * 100;
            line.LineDiscount = l.LineDiscountPct;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                var disc = lineAmt * (l.LineDiscountPct / 100m);
                line.LineDiscountAmount = Math.Round(disc, 2);
                lineAmt -= disc;
            }
            line.Amount = Math.Round(lineAmt, 2);
            line.AmountIncludingVat = Math.Round(lineAmt * (1 + VatRate), 2);
            line.OutstandingQuantity = l.Quantity;
            line.QtyToInvoice = l.Quantity;
            line.QtyToShip = l.Quantity;
            line.OutstandingAmount = line.Amount;

            totalAmount += line.Amount;
            totalAmountVat += line.AmountIncludingVat;

            await lineRepo.AddAsync(line, ct);
            lineNo += 10000;
        }

        header.Amount = Math.Round(totalAmount, 2);
        header.AmountIncludingVat = Math.Round(totalAmountVat, 2);
        await headerRepo.UpdateAsync(header, ct);
        await uow.SaveChangesAsync(ct);

        return new UpdateSalesOrderLinesResult(header.Amount, header.AmountIncludingVat);
    }
}
