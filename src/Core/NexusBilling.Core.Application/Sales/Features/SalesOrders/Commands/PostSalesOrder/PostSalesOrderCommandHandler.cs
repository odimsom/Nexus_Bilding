using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Application.Ecf.Interfaces;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Ecf.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.PostSalesOrder;

public sealed class PostSalesOrderCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    ISalesInvoiceHeaderRepository invoiceHeaderRepo,
    ISalesInvoiceLineRepository invoiceLineRepo,
    IEcfCompanyConfigRepository ecfConfigRepo,
    IEcfDocumentRepository ecfDocRepo,
    IEcfService ecfService,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<PostSalesOrderCommand, PostSalesOrderResult>
{
    public async Task<PostSalesOrderResult> Handle(PostSalesOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        // 1. Fetch Order
        var order = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct)
            ?? throw new InvalidOperationException($"Orden {cmd.No} no encontrada.");

        if (order.Status != "Open" && order.Status != "Released")
            throw new InvalidOperationException($"La orden {cmd.No} debe estar Abierta o Lanzada para contabilizarse.");

        var lines = (await lineRepo.GetByDocumentNoAsync(1 /* Order */, cmd.No, ct)).ToList();
        if (lines.Count == 0)
            throw new InvalidOperationException("La orden no tiene líneas. Agrega al menos una línea antes de contabilizar.");

        // 2. Generate invoice number via No-Series
        var invoiceNo = await noSeriesService.GetNextNoAsync(cmd.TenantId, "FS", ct);
        
        var invoiceHeaderResult = SalesInvoiceHeader.Create(tid);
        if (!invoiceHeaderResult.IsSuccess)
            throw new InvalidOperationException("Error creando cabecera de factura.");

        var invoiceHeader = invoiceHeaderResult.GetValue()!;
        invoiceHeader.No = invoiceNo;
        invoiceHeader.SellToCustomerNo = order.SellToCustomerNo;
        invoiceHeader.SellToCustomerName = order.SellToCustomerName;
        invoiceHeader.BillToName = order.BillToName ?? order.SellToCustomerName;
        invoiceHeader.PostingDate = DateTime.UtcNow;
        invoiceHeader.DueDate = order.DueDate;
        invoiceHeader.Amount = order.Amount;
        invoiceHeader.AmountIncludingVat = order.AmountIncludingVat;
        invoiceHeader.CurrencyCode = order.CurrencyCode;
        invoiceHeader.PaymentTermsCode = order.PaymentTermsCode;
        invoiceHeader.PaymentMethodCode = order.PaymentMethodCode;
        invoiceHeader.SalespersonCode = order.SalespersonCode;
        invoiceHeader.ExternalDocumentNo = order.ExternalDocumentNo;
        invoiceHeader.OrderNo = order.No;

        await invoiceHeaderRepo.AddAsync(invoiceHeader, ct);

        var invoiceLines = new List<SalesInvoiceLine>();
        int lineNo = 10000;
        foreach (var l in lines)
        {
            var invLineResult = SalesInvoiceLine.Create(tid);
            if (!invLineResult.IsSuccess) continue;

            var invLine = invLineResult.GetValue()!;
            invLine.DocumentNo = invoiceNo;
            invLine.LineNo = lineNo;
            invLine.No = l.No ?? string.Empty;
            invLine.Description = l.Description;
            invLine.Quantity = l.Quantity;
            invLine.UnitPrice = l.UnitPrice;
            invLine.LineDiscount = l.LineDiscount;
            invLine.Amount = l.Amount;
            invLine.AmountIncludingVat = l.AmountIncludingVat;
            invLine.UnitOfMeasureCode = l.UnitOfMeasure;
            invLine.Vat = l.Vat;
            lineNo += 10000;

            await invoiceLineRepo.AddAsync(invLine, ct);
            invoiceLines.Add(invLine);
        }

        // 3. ECF Logic
        bool ecfProcessed = false;
        string? trackId = null;

        var ecfConfigs = await ecfConfigRepo.GetAllAsync(null, ct);
        var ecfConfig = ecfConfigs.FirstOrDefault(c => c.TenantId == NexusBilling.Core.Domain.Common.TenantIdentifier.Create(cmd.TenantId) && c.IsActive);

        if (ecfConfig != null)
        {
            // Generate XML
            var xmlResult = await ecfService.GenerateSalesInvoiceXmlAsync(tid, invoiceHeader, invoiceLines);
            if (xmlResult.IsSuccess)
            {
                var signedResult = await ecfService.SignXmlAsync(xmlResult.GetValue()!, ecfConfig.P12Path, "password_dummy");
                if (signedResult.IsSuccess)
                {
                    var submissionResult = await ecfService.SendToDgiiAsync(signedResult.GetValue()!, ecfConfig.Rnc, ecfConfig.Environment.ToString());
                    if (submissionResult.IsSuccess)
                    {
                        trackId = submissionResult.GetValue();
                        ecfProcessed = true;

                        // Save ECF Document
                        var ecfDocResult = EcfDocument.Create(tid, invoiceHeader.Id, invoiceNo, signedResult.GetValue()!);
                        if (ecfDocResult.IsSuccess)
                        {
                            var ecfDoc = ecfDocResult.GetValue()!;
                            ecfDoc.MarkAsSent(trackId!);
                            await ecfDocRepo.AddAsync(ecfDoc, ct);
                        }
                    }
                }
            }
        }

        // 4. Delete Order
        await headerRepo.DeleteAsync(order, ct);
        foreach(var l in lines) await lineRepo.DeleteAsync(l, ct);

        await uow.SaveChangesAsync(ct);
        return new PostSalesOrderResult(invoiceNo, ecfProcessed, trackId);
    }
}
