import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ServiceOrderService } from '../../../data/service-order.service';
import { ServiceOrderDetail } from '../../../domain/service-order.model';
import { PdfService } from '../../../../../shared/services/pdf.service';

@Component({
  selector: 'app-service-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './service-order-card.page.html',
  styleUrl: './service-order-card.page.css'
})
export class ServiceOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(ServiceOrderService);
  private readonly pdfSvc = inject(PdfService);

  order = signal<ServiceOrderDetail | null>(null);
  loading = signal(true);
  working = signal(false);
  printing = signal(false);
  actionError = signal('');

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    try {
      const detail = await this.svc.getDetail(no);
      this.order.set(detail);
    } catch {
      this.order.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  statusClass(status: number): string {
    return ({ 0: 'nx-badge--outline', 1: 'nx-badge--info', 2: 'nx-badge--success', 3: 'nx-badge--warn' } as Record<number, string>)[status] ?? 'nx-badge--outline';
  }

  async updateStatus(newStatus: number): Promise<void> {
    const o = this.order();
    if (!o || this.working()) return;
    this.working.set(true);
    this.actionError.set('');
    try {
      await this.svc.updateStatus(o.no, newStatus);
      this.order.set(await this.svc.getDetail(o.no));
    } catch (e: any) {
      this.actionError.set(e?.error?.error?.message ?? 'Error al actualizar el estado.');
    } finally {
      this.working.set(false);
    }
  }

  async invoiceOrder(): Promise<void> {
    const o = this.order();
    if (!o || this.working()) return;
    this.working.set(true);
    this.actionError.set('');
    try {
      const invoiceNo = await this.svc.invoiceServiceOrder(o.no);
      this.router.navigate(['/invoices', invoiceNo]);
    } catch (e: any) {
      this.actionError.set(e?.error?.error?.message ?? 'Error al facturar la orden.');
      this.working.set(false);
    }
  }

  async printPdf(): Promise<void> {
    const o = this.order();
    if (!o || this.printing()) return;
    this.printing.set(true);
    try {
      await this.pdfSvc.printServiceOrder({
        no: o.no,
        customerName: o.customerName,
        customerNo: o.customerNo,
        orderDate: o.orderDate,
        startingDate: o.startingDate,
        finishingDate: o.finishingDate,
        paymentTermsCode: o.paymentTermsCode,
        currencyCode: o.currencyCode || 'DOP',
        status: o.statusLabel,
        description: o.description,
        contractNo: o.contractNo,
        lines: o.lines.map(l => ({
          lineNo: l.lineNo,
          no: l.no,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscount: l.lineDiscount,
          amount: l.amount,
          amountIncludingVat: l.amountIncludingVat,
          unitOfMeasure: l.unitOfMeasure,
        })),
        amount: o.amount,
        amountIncludingVat: o.amountIncludingVat,
      });
    } finally {
      this.printing.set(false);
    }
  }
}
