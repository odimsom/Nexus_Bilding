import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InvoiceService } from '../../../data/invoice.service';
import { PdfService } from '../../../../../shared/services/pdf.service';

@Component({
  selector: 'app-invoice-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './invoice-card.page.html',
  styleUrl: './invoice-card.page.css'
})
export class InvoiceCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(InvoiceService);
  private readonly pdfSvc = inject(PdfService);

  invoice = signal<any | null>(null);
  loading = signal(true);
  printing = signal(false);

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    try {
      this.invoice.set(await this.svc.getInvoiceDetail(no));
    } catch {
      this.invoice.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  async printPdf(): Promise<void> {
    const inv = this.invoice();
    if (!inv || this.printing()) return;
    this.printing.set(true);
    try {
      await this.pdfSvc.printInvoice({
        no: inv.no,
        orderNo: inv.orderNo,
        customerName: inv.sellToCustomerName,
        customerNo: inv.sellToCustomerNo,
        billToName: inv.billToName,
        externalDocumentNo: inv.externalDocumentNo,
        salespersonCode: inv.salespersonCode,
        postingDate: inv.postingDate,
        dueDate: inv.dueDate,
        paymentTerms: inv.paymentTermsCode,
        paymentMethodCode: inv.paymentMethodCode,
        currencyCode: inv.currencyCode || 'DOP',
        status: 'posted',
        lines: (inv.lines ?? []).map((l: any) => ({
          lineNo: l.lineNo,
          type: l.type,
          no: l.no,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct ?? 0,
          amount: l.amount,
          vat: l.amountIncludingVat - l.amount,
          amountIncludingVat: l.amountIncludingVat,
          unitOfMeasureCode: l.unitOfMeasureCode,
        })),
        amount: inv.amount,
        amountIncludingVat: inv.amountIncludingVat,
        remainingAmount: inv.remainingAmount ?? 0,
      });
    } finally {
      this.printing.set(false);
    }
  }
}
