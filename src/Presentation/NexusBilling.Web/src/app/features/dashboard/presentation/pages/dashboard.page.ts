import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import { DashboardService } from '../../data/dashboard.service';
import { NxChartComponent } from '../../../../shared/components/chart/nx-chart.component';

const DEMO_MONTHS = ['Ene','Feb','Mar','Abr','May','Jun','Jul','Ago','Sep','Oct','Nov','Dic'];
const DEMO_SALES   = [185000,220000,198000,245000,310000,275000,295000,320000,285000,340000,365000,410000];
const DEMO_PURCH   = [120000,145000,130000,160000,195000,175000,185000,200000,170000,215000,230000,260000];

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, NxChartComponent],
  templateUrl: './dashboard.page.html',
  styleUrl: './dashboard.page.css'
})
export class DashboardPage implements OnInit {
  readonly svc = inject(DashboardService);

  readonly quickLinks = [
    { route: '/customers',         label: 'Clientes',           desc: 'Gestiona tu cartera',     icon: 'users',     bg: 'var(--nx-blue-50)',    color: 'var(--nx-blue-500)'   },
    { route: '/inventory',         label: 'Inventario',         desc: 'Artículos y existencias', icon: 'package',   bg: 'var(--nx-emerald-50)', color: 'var(--nx-action)'     },
    { route: '/sales',             label: 'Órdenes de Venta',   desc: 'Pedidos de clientes',     icon: 'file-text', bg: 'var(--nx-amber-50)',   color: 'var(--nx-amber-500)'  },
    { route: '/quotes',            label: 'Cotizaciones',       desc: 'Presupuestos enviados',   icon: 'quote',     bg: 'var(--nx-blue-50)',    color: 'var(--nx-blue-500)'   },
    { route: '/invoices',          label: 'Facturas de Venta',  desc: 'Facturas emitidas',       icon: 'receipt',   bg: 'var(--nx-indigo-50)',  color: 'var(--nx-indigo-600)' },
    { route: '/purchases',         label: 'Órdenes de Compra',  desc: 'Pedidos a proveedores',   icon: 'file-text', bg: 'var(--nx-orange-50)',  color: 'var(--nx-orange-600)' },
    { route: '/purchase-invoices', label: 'Facturas de Compra', desc: 'Facturas recibidas',      icon: 'receipt',   bg: 'var(--nx-teal-50)',    color: 'var(--nx-teal-600)'   },
    { route: '/settings',          label: 'Config.',            desc: 'Empresa y secuencias',    icon: 'settings',  bg: 'var(--nx-canvas-alt)', color: 'var(--nx-text-muted)' },
  ];

  readonly usingDemo = computed(() => {
    const s = this.svc.stats();
    return !s?.monthlySales?.length && !s?.monthlyPurchases?.length;
  });

  readonly comparisonChartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    interaction: { mode: 'index', intersect: false },
    plugins: {
      legend: { position: 'top', labels: { usePointStyle: true, padding: 16, font: { size: 11 } } },
      tooltip: {
        callbacks: {
          label: (ctx) => ` ${ctx.dataset.label}: DOP ${(ctx.raw as number).toLocaleString('es-DO', { minimumFractionDigits: 0 })}`
        }
      }
    },
    scales: {
      x: { grid: { display: false }, ticks: { font: { size: 11 } } },
      y: {
        grid: { color: 'rgba(128,128,128,0.1)' },
        ticks: {
          font: { size: 10 },
          callback: (v) => `${(Number(v) / 1000).toFixed(0)}k`
        }
      }
    }
  };

  readonly comparisonChartData = computed<ChartData>(() => {
    const stats = this.svc.stats();
    const monthNames: Record<string, string> = {
      '01':'Ene','02':'Feb','03':'Mar','04':'Abr','05':'May','06':'Jun',
      '07':'Jul','08':'Ago','09':'Sep','10':'Oct','11':'Nov','12':'Dic'
    };

    if (!stats?.monthlySales?.length && !stats?.monthlyPurchases?.length) {
      return {
        labels: DEMO_MONTHS,
        datasets: [
          { label: 'Ventas', data: DEMO_SALES, backgroundColor: 'rgba(12,113,86,0.8)', borderColor: '#0C7156', borderWidth: 1, borderRadius: 4, hoverBackgroundColor: '#0C7156' },
          { label: 'Compras', data: DEMO_PURCH, backgroundColor: 'rgba(44,99,214,0.65)', borderColor: '#2C63D6', borderWidth: 1, borderRadius: 4, hoverBackgroundColor: '#2C63D6' },
        ]
      };
    }

    const allMonths = new Set([
      ...(stats.monthlySales ?? []).map(m => m.month),
      ...(stats.monthlyPurchases ?? []).map(m => m.month),
    ]);
    const sortedMonths = [...allMonths].sort();

    const salesMap = new Map((stats.monthlySales ?? []).map(m => [m.month, m.total]));
    const purchMap = new Map((stats.monthlyPurchases ?? []).map(m => [m.month, m.total]));

    return {
      labels: sortedMonths.map(m => { const [, mo] = m.split('-'); return monthNames[mo] ?? mo; }),
      datasets: [
        { label: 'Ventas', data: sortedMonths.map(m => salesMap.get(m) ?? 0), backgroundColor: 'rgba(12,113,86,0.8)', borderColor: '#0C7156', borderWidth: 1, borderRadius: 4, hoverBackgroundColor: '#0C7156' },
        { label: 'Compras', data: sortedMonths.map(m => purchMap.get(m) ?? 0), backgroundColor: 'rgba(44,99,214,0.65)', borderColor: '#2C63D6', borderWidth: 1, borderRadius: 4, hoverBackgroundColor: '#2C63D6' },
      ]
    };
  });

  readonly statusChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    cutout: '65%',
    plugins: {
      legend: { position: 'bottom', labels: { usePointStyle: true, padding: 16, font: { size: 11 } } },
      tooltip: {
        callbacks: {
          label: (ctx: any) => ` ${ctx.label}: ${ctx.raw} órdenes`
        }
      }
    }
  };

  readonly statusChartData = computed<ChartData>(() => {
    const stats = this.svc.stats();
    if (!stats?.recentOrders) return { labels: [], datasets: [] };

    let open = 0, released = 0, closed = 0;
    stats.recentOrders.forEach(o => {
      if (o.status === 'Open') open++;
      else if (o.status === 'Released') released++;
      else closed++;
    });
    open = stats.openOrders > open ? stats.openOrders : open;

    if (open + released + closed === 0) {
      open = 5; released = 2; closed = 3;
    }

    return {
      labels: ['Abiertas', 'Liberadas', 'Cerradas'],
      datasets: [{ data: [open, released, closed], backgroundColor: ['#3B82F6','#10B981','#94A3B8'], borderWidth: 0, hoverOffset: 4 }]
    };
  });

  readonly margin = computed(() => {
    const s = this.svc.stats();
    if (!s) return 0;
    const sales = s.totalSalesAllTime || (DEMO_SALES.reduce((a,b) => a+b, 0));
    const purch = s.totalPurchasesAllTime || (DEMO_PURCH.reduce((a,b) => a+b, 0));
    return sales - purch;
  });

  readonly marginColor = computed(() => this.margin() >= 0 ? 'var(--nx-action)' : 'var(--nx-red-500)');

  readonly bestSalesMonth = computed(() => {
    const s = this.svc.stats();
    if (s?.monthlySales?.length) {
      return Math.max(...s.monthlySales.map(m => m.total));
    }
    return Math.max(...DEMO_SALES);
  });

  async ngOnInit(): Promise<void> { await this.svc.load(); }
  async reload(): Promise<void> { await this.svc.load(); }

  statusClass(s: string): string {
    return { Open:'nx-badge--info', Released:'nx-badge--success', Closed:'nx-badge--outline' }[s] ?? 'nx-badge--outline';
  }
  statusLabel(s: string): string {
    return { Open:'Abierta', Released:'Liberada', Closed:'Cerrada' }[s] ?? s;
  }
}
