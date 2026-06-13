using MediatR;
using System.Reflection;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseInvoices.Commands.CreatePurchaseInvoice;

public sealed class CreatePurchaseInvoiceCommandHandler(
    IPurchInvHeaderRepository headerRepo,
    IPurchInvLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<CreatePurchaseInvoiceCommand, CreatePurchaseInvoiceResult>
{
    public async Task<CreatePurchaseInvoiceResult> Handle(CreatePurchaseInvoiceCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        // Validate duplicate external document number for same vendor
        if (!string.IsNullOrWhiteSpace(cmd.ExternalDocumentNo))
        {
            var existing = await headerRepo.FindAsync(
                h => h.TenantId == tid &&
                     h.BuyFromVendorNo == cmd.BuyFromVendorNo &&
                     h.VendorInvoiceNo == cmd.ExternalDocumentNo,
                cancellationToken: ct);

            if (existing.Count > 0)
                throw new InvalidOperationException(
                    $"Ya existe una factura del proveedor {cmd.BuyFromVendorNo} con el número de referencia '{cmd.ExternalDocumentNo}'.");
        }

        if (!cmd.Lines.Any())
            throw new InvalidOperationException("La factura debe tener al menos una línea.");

        var invoiceNo = await noSeriesService.GetNextNoAsync(cmd.TenantId, "FC", ct);

        // Build posted invoice header
        var headerResult = PurchInvHeader.Create(tid);
        if (!headerResult.IsSuccess)
            throw new InvalidOperationException("Error al crear cabecera de factura.");

        var header = headerResult.GetValue()!;
        SetProp(header, "No", invoiceNo);
        SetProp(header, "BuyFromVendorNo", cmd.BuyFromVendorNo);
        SetProp(header, "PayToName", cmd.PayToName);
        SetProp(header, "PostingDate", DateTime.SpecifyKind(cmd.PostingDate, DateTimeKind.Utc));
        SetProp(header, "VendorInvoiceNo", cmd.ExternalDocumentNo ?? string.Empty);
        SetProp(header, "PaymentTermsCode", cmd.PaymentTermsCode ?? string.Empty);
        SetProp(header, "CurrencyCode", cmd.CurrencyCode ?? string.Empty);

        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        var lineEntities = new List<PurchInvLine>();
        foreach (var l in cmd.Lines)
        {
            var lineResult = PurchInvLine.Create(tid);
            if (!lineResult.IsSuccess) continue;

            var line = lineResult.GetValue()!;
            decimal vatRate = l.VatPct / 100m;
            decimal lineAmt = Math.Round(l.Quantity * l.UnitCost, 2);
            decimal lineAmtVat = Math.Round(lineAmt * (1 + vatRate), 2);

            SetProp(line, "DocumentNo", invoiceNo);
            SetProp(line, "BuyFromVendorNo", cmd.BuyFromVendorNo);
            SetProp(line, "LineNo", lineNo);
            SetProp(line, "Description", l.Description);
            SetProp(line, "Quantity", l.Quantity);
            SetProp(line, "DirectUnitCost", l.UnitCost);
            SetProp(line, "UnitCostLcy", l.UnitCost);
            SetProp(line, "UnitOfMeasure", l.UnitOfMeasureCode);
            SetProp(line, "Vat", l.VatPct);
            SetProp(line, "Amount", lineAmt);
            SetProp(line, "AmountIncludingVat", lineAmtVat);

            totalAmount += lineAmt;
            totalAmountVat += lineAmtVat;
            lineEntities.Add(line);
            lineNo += 10000;
        }

        SetProp(header, "Amount", Math.Round(totalAmount, 2));
        SetProp(header, "AmountIncludingVat", Math.Round(totalAmountVat, 2));

        await headerRepo.AddAsync(header, ct);
        foreach (var line in lineEntities)
            await lineRepo.AddAsync(line, ct);

        await uow.SaveChangesAsync(ct);
        return new CreatePurchaseInvoiceResult(invoiceNo);
    }

    private static void SetProp(object obj, string name, object value)
    {
        var prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        prop?.SetValue(obj, value);
    }
}
