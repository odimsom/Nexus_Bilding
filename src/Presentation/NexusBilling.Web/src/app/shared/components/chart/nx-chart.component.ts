import {
  Component, ElementRef, Input, OnChanges, OnDestroy, ViewChild, AfterViewInit
} from '@angular/core';
import {
  Chart, ChartType, ChartData, ChartOptions,
  CategoryScale, LinearScale, BarElement, LineElement, PointElement,
  Tooltip, Legend, Filler, BarController, LineController, DoughnutController, ArcElement
} from 'chart.js';

Chart.register(CategoryScale, LinearScale, BarElement, LineElement, PointElement, Tooltip, Legend, Filler, BarController, LineController, DoughnutController, ArcElement);

@Component({
  selector: 'nx-chart',
  standalone: true,
  template: `<canvas #canvas style="width:100%;height:100%;display:block;"></canvas>`,
  styles: [`:host { display: block; position: relative; }`]
})
export class NxChartComponent implements AfterViewInit, OnChanges, OnDestroy {
  @ViewChild('canvas') canvasRef!: ElementRef<HTMLCanvasElement>;

  @Input() type: ChartType = 'bar';
  @Input() data: ChartData = { labels: [], datasets: [] };
  @Input() options: ChartOptions = {};

  private chart: Chart | null = null;

  ngAfterViewInit(): void { this.build(); }

  ngOnChanges(): void {
    if (this.chart) {
      this.chart.data = this.data;
      this.chart.update('active');
    }
  }

  ngOnDestroy(): void { this.chart?.destroy(); }

  private build(): void {
    const ctx = this.canvasRef.nativeElement.getContext('2d');
    if (!ctx) return;
    this.chart = new Chart(ctx, { type: this.type, data: this.data, options: this.options });
  }
}
