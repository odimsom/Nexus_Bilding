using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Administration.Entities;
using NexusBilling.Core.Domain.Administration.Repositories;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Administration.Commands;

public sealed class CreateServiceOrderCommandHandler(
    IServiceHeaderRepository headerRepo,
    IServiceLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<CreateServiceOrderCommand, CreateServiceOrderResult>
{
    private const decimal VatRate = 0.18m;

    public async Task<CreateServiceOrderResult> Handle(CreateServiceOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        string docNo = cmd.SeriesCode is not null
            ? await noSeriesService.GetNextNoAsync(cmd.TenantId, cmd.SeriesCode, ct)
            : cmd.ManualNo ?? throw new InvalidOperationException("Se requiere SeriesCode o ManualNo.");

        var headerResult = ServiceHeader.Create(tid);
        if (!headerResult.IsSuccess)
            throw new InvalidOperationException(headerResult.GetError()!.Message);

        static DateTime Utc(DateTime d) => DateTime.SpecifyKind(d, DateTimeKind.Utc);
        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        var header = headerResult.GetValue()!;
        header.No = docNo;
        header.DocumentType = cmd.DocumentType;
        header.CustomerNo = cmd.CustomerNo;
        header.Name = cmd.CustomerName;
        header.BillToCustomerNo = cmd.CustomerNo;
        header.BillToName = cmd.CustomerName;
        header.Description = cmd.Description;
        header.OrderDate = Utc(cmd.OrderDate);
        header.PostingDate = Utc(cmd.OrderDate);
        header.StartingDate = UtcN(cmd.StartingDate);
        header.FinishingDate = UtcN(cmd.FinishingDate);
        header.PaymentTermsCode = cmd.PaymentTermsCode ?? string.Empty;
        header.PaymentMethodCode = cmd.PaymentMethodCode ?? string.Empty;
        header.SalespersonCode = cmd.SalespersonCode ?? string.Empty;
        header.CurrencyCode = cmd.CurrencyCode ?? string.Empty;
        header.ContractNo = cmd.ContractNo ?? string.Empty;
        header.Status = 0; // Pending
        header.CreatedAt = DateTime.UtcNow;
        header.UpdatedAt = DateTime.UtcNow;

        int lineNo = 10000;
        var lineEntities = new List<ServiceLine>();

        foreach (var l in cmd.Lines)
        {
            var lineResult = ServiceLine.Create(tid);
            if (!lineResult.IsSuccess) continue;
            var line = lineResult.GetValue()!;
            line.DocumentType = cmd.DocumentType;
            line.DocumentNo = docNo;
            line.CustomerNo = cmd.CustomerNo;
            line.LineNo = lineNo;
            line.Type = l.LineType;
            line.No = l.No ?? string.Empty;
            line.Description = l.Description;
            line.UnitOfMeasure = l.UnitOfMeasure;
            line.Quantity = l.Quantity;
            line.UnitPrice = l.UnitPrice;
            line.Vat = VatRate * 100;
            line.LineDiscount = l.LineDiscountPct;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                decimal discAmt = lineAmt * (l.LineDiscountPct / 100m);
                line.LineDiscountAmount = Math.Round(discAmt, 2);
                lineAmt -= discAmt;
            }
            line.Amount = Math.Round(lineAmt, 2);
            line.AmountIncludingVat = Math.Round(lineAmt * (1 + VatRate), 2);
            line.OutstandingQuantity = l.Quantity;
            line.QtyToInvoice = l.Quantity;
            line.QtyToShip = l.Quantity;
            line.OutstandingAmount = line.Amount;
            line.CreatedAt = DateTime.UtcNow;
            line.UpdatedAt = DateTime.UtcNow;

            lineEntities.Add(line);
            lineNo += 10000;
        }

        await headerRepo.AddAsync(header, ct);
        foreach (var line in lineEntities)
            await lineRepo.AddAsync(line, ct);

        await uow.SaveChangesAsync(ct);
        return new CreateServiceOrderResult(docNo);
    }
}
