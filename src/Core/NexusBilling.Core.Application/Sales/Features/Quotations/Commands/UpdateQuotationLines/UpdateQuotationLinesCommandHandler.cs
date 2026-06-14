using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.UpdateQuotationLines;

public sealed class UpdateQuotationLinesCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdateQuotationLinesCommand, UpdateQuotationLinesResult>
{
    public async Task<UpdateQuotationLinesResult> Handle(UpdateQuotationLinesCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var header = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.QuotationNo, ct)
            ?? throw new InvalidOperationException($"La cotización {cmd.QuotationNo} no existe.");

        if (header.DocumentType != "Quote")
            throw new InvalidOperationException($"El documento {cmd.QuotationNo} no es una cotización.");

        if (header.Status != "Open")
            throw new InvalidOperationException("Solo se pueden editar líneas de cotizaciones en estado Abierta.");

        var existingLines = await lineRepo.GetByDocumentNoAsync(0, cmd.QuotationNo, ct);
        foreach (var line in existingLines)
            await lineRepo.DeleteAsync(line, ct);

        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        foreach (var l in cmd.Lines)
        {
            var lineResult = SalesLine.Create(tid, 0, cmd.QuotationNo, lineNo);
            if (!lineResult.IsSuccess) continue;

            var vatRate = (l.VatPct > 0 ? l.VatPct : 18m) / 100m;
            var line = lineResult.GetValue()!;
            line.SellToCustomerNo = header.SellToCustomerNo;
            line.Type = l.LineType switch { "Service" => 3, "G/L Account" => 1, _ => 2 };
            line.No = l.ItemNo ?? string.Empty;
            line.Description = l.Description;
            line.UnitOfMeasure = l.UnitOfMeasure;
            line.Quantity = l.Quantity;
            line.UnitPrice = l.UnitPrice;
            line.Vat = l.VatPct > 0 ? l.VatPct : 18m;
            line.LineDiscount = l.LineDiscountPct;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                line.LineDiscountAmount = Math.Round(lineAmt * (l.LineDiscountPct / 100m), 2);
                lineAmt -= line.LineDiscountAmount;
            }
            line.Amount = Math.Round(lineAmt, 2);
            line.AmountIncludingVat = Math.Round(lineAmt * (1 + vatRate), 2);

            totalAmount += line.Amount;
            totalAmountVat += line.AmountIncludingVat;
            lineNo += 10000;

            await lineRepo.AddAsync(line, ct);
        }

        header.Amount = Math.Round(totalAmount, 2);
        header.AmountIncludingVat = Math.Round(totalAmountVat, 2);
        await headerRepo.UpdateAsync(header, ct);

        await uow.SaveChangesAsync(ct);
        return new UpdateQuotationLinesResult(header.Amount, header.AmountIncludingVat);
    }
}
