import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ServiceOrderService } from '../../../data/service-order.service';
import { ServiceOrderDetail } from '../../../domain/service-order.model';

@Component({
  selector: 'app-service-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './service-order-card.page.html',
  styleUrl: './service-order-card.page.css'
})
export class ServiceOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(ServiceOrderService);

  order = signal<ServiceOrderDetail | null>(null);
  loading = signal(true);

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
}
