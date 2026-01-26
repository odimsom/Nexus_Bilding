import { useState } from 'react';
import { FileText, Download, DollarSign, Users, Package, TrendingUp, TrendingDown, Calendar } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { Badge } from '../../core/components/ui/Badge';

type ReportPeriod = 'week' | 'month' | 'quarter' | 'year';

interface ReportMetric {
  label: string;
  value: string;
  change: number;
  icon: React.ReactNode;
}

export function Reports() {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const [period, setPeriod] = useState<ReportPeriod>('month');

  // Mock data for reports
  const metrics: ReportMetric[] = [
    {
      label: 'Total Revenue',
      value: '$124,500',
      change: 12.5,
      icon: <DollarSign className="w-5 h-5" />,
    },
    {
      label: 'New Clients',
      value: '28',
      change: 8.2,
      icon: <Users className="w-5 h-5" />,
    },
    {
      label: 'Products Sold',
      value: '342',
      change: -3.1,
      icon: <Package className="w-5 h-5" />,
    },
    {
      label: 'Fiscal Docs Issued',
      value: '156',
      change: 15.3,
      icon: <FileText className="w-5 h-5" />,
    },
  ];

  const topProducts = [
    { name: 'Ergonomic Office Chair', revenue: '$14,560', units: 42 },
    { name: 'Standing Desk Pro', revenue: '$12,340', units: 28 },
    { name: 'Monitor Arm Dual', revenue: '$8,920', units: 52 },
    { name: 'Wireless Keyboard', revenue: '$6,780', units: 89 },
    { name: 'USB-C Hub', revenue: '$5,240', units: 112 },
  ];

  const topClients = [
    { name: 'Acme Studio', revenue: '$34,520', invoices: 12 },
    { name: 'Tech Innovations Co.', revenue: '$28,940', invoices: 8 },
    { name: 'Design Hub LLC', revenue: '$22,650', invoices: 15 },
    { name: 'Global Solutions', revenue: '$19,870', invoices: 7 },
    { name: 'Creative Agency', revenue: '$16,230', invoices: 9 },
  ];

  return (
    <DashboardLayout title="Reports" user={user}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        {/* Header Section */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-2.5">
            <FileText className="w-10 h-10 text-primary" />
            <div className="flex items-center gap-2">
              <Calendar className="w-5 h-5 text-matte-text-muted" />
              <span className="text-sm font-medium text-matte-text">
                {period === 'week' && 'Last 7 days'}
                {period === 'month' && 'Last 30 days'}
                {period === 'quarter' && 'Last 90 days'}
                {period === 'year' && 'Last 12 months'}
              </span>
            </div>
          </div>
          <button
            className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm"
            onClick={() => {
              // Basic CSV export of metrics
              const headers = ['Metric', 'Value', 'Change'];
              const rows = metrics.map(m => [m.label, m.value, `${m.change}%`]);
              const csvContent = "data:text/csv;charset=utf-8," 
                + headers.join(",") + "\n" 
                + rows.map(e => e.join(",")).join("\n");
              
              const encodedUri = encodeURI(csvContent);
              const link = document.createElement("a");
              link.setAttribute("href", encodedUri);
              link.setAttribute("download", `nexus_report_${period}.csv`);
              document.body.appendChild(link);
              link.click();
              document.body.removeChild(link);
            }}
          >
            <Download className="w-4 h-4" /> Export Report
          </button>
        </div>

        {/* Period Filters */}
        <div className="flex gap-2">
          <button
            className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
              period === 'week'
                ? 'bg-primary text-white border-primary'
                : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
            }`}
            onClick={() => setPeriod('week')}
          >
            Week
          </button>
          <button
            className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
              period === 'month'
                ? 'bg-primary text-white border-primary'
                : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
            }`}
            onClick={() => setPeriod('month')}
          >
            Month
          </button>
          <button
            className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
              period === 'quarter'
                ? 'bg-primary text-white border-primary'
                : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
            }`}
            onClick={() => setPeriod('quarter')}
          >
            Quarter
          </button>
          <button
            className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
              period === 'year'
                ? 'bg-primary text-white border-primary'
                : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
            }`}
            onClick={() => setPeriod('year')}
          >
            Year
          </button>
        </div>

        {/* Metrics Cards */}
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
          {metrics.map((metric, index) => (
            <Card key={index}>
              <div className="flex items-center justify-between mb-3">
                <p className="text-sm font-medium text-matte-text-muted">{metric.label}</p>
                <div className="text-matte-text-muted">{metric.icon}</div>
              </div>
              <p className="text-2xl font-bold text-matte-text mb-2">{metric.value}</p>
              <div className="flex items-center gap-1">
                {metric.change >= 0 ? (
                  <>
                    <TrendingUp className="w-4 h-4 text-ink-paid-text" />
                    <p className="text-sm font-medium text-ink-paid-text">
                      +{metric.change}%
                    </p>
                  </>
                ) : (
                  <>
                    <TrendingDown className="w-4 h-4 text-ink-error-text" />
                    <p className="text-sm font-medium text-ink-error-text">
                      {metric.change}%
                    </p>
                  </>
                )}
                <span className="text-sm text-matte-text-muted ml-1">vs last period</span>
              </div>
            </Card>
          ))}
        </div>

        {/* Top Products & Top Clients */}
        <div className="grid grid-cols-1 gap-8 lg:grid-cols-2">
          {/* Top Products */}
          <Card>
            <div className="mb-4 flex items-center justify-between">
              <h3 className="text-lg font-bold text-matte-text">Top Products</h3>
              <Badge status="active">By Revenue</Badge>
            </div>
            <div className="flex flex-col gap-3">
              {topProducts.map((product, index) => (
                <div
                  key={index}
                  className="flex items-center justify-between border-b border-matte-border/50 pb-3 last:border-0 last:pb-0"
                >
                  <div className="flex items-center gap-3">
                    <span className="flex items-center justify-center w-6 h-6 rounded-full bg-matte-base border border-matte-border text-xs font-bold text-matte-text">
                      {index + 1}
                    </span>
                    <div className="flex flex-col">
                      <p className="text-sm font-medium text-matte-text">{product.name}</p>
                      <p className="text-xs text-matte-text-muted">{product.units} units sold</p>
                    </div>
                  </div>
                  <p className="text-sm font-bold text-matte-text">{product.revenue}</p>
                </div>
              ))}
            </div>
          </Card>

          {/* Top Clients */}
          <Card>
            <div className="mb-4 flex items-center justify-between">
              <h3 className="text-lg font-bold text-matte-text">Top Clients</h3>
              <Badge status="active">By Revenue</Badge>
            </div>
            <div className="flex flex-col gap-3">
              {topClients.map((client, index) => (
                <div
                  key={index}
                  className="flex items-center justify-between border-b border-matte-border/50 pb-3 last:border-0 last:pb-0"
                >
                  <div className="flex items-center gap-3">
                    <span className="flex items-center justify-center w-6 h-6 rounded-full bg-matte-base border border-matte-border text-xs font-bold text-matte-text">
                      {index + 1}
                    </span>
                    <div className="flex flex-col">
                      <p className="text-sm font-medium text-matte-text">{client.name}</p>
                      <p className="text-xs text-matte-text-muted">{client.invoices} docs</p>
                    </div>
                  </div>
                  <p className="text-sm font-bold text-matte-text">{client.revenue}</p>
                </div>
              ))}
            </div>
          </Card>
        </div>

        {/* Revenue Summary */}
        <Card>
          <div className="mb-6 flex items-center justify-between">
            <h3 className="text-lg font-bold text-matte-text">Revenue Summary</h3>
            <select className="rounded border-matte-border bg-matte-input px-3 py-1 text-sm text-matte-text focus:border-primary focus:ring-0">
              <option>All Categories</option>
              <option>Office Furniture</option>
              <option>Electronics</option>
              <option>Accessories</option>
            </select>
          </div>
          <div className="relative h-[300px] w-full">
            <svg className="h-full w-full overflow-visible" preserveAspectRatio="none" viewBox="0 0 800 300">
              <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="250" y2="250" />
              <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="190" y2="190" />
              <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="130" y2="130" />
              <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="70" y2="70" />
              <path
                d="M0 220 L160 200 L320 170 L480 140 L640 110 L800 80"
                fill="none"
                stroke="#8c92ab"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="3"
                vectorEffect="non-scaling-stroke"
              />
              <circle cx="160" cy="200" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
              <circle cx="320" cy="170" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
              <circle cx="480" cy="140" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
              <circle cx="640" cy="110" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
              <circle cx="800" cy="80" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
            </svg>
            <div className="absolute -bottom-6 left-0 right-0 flex justify-between text-xs font-medium text-matte-text-muted">
              {period === 'week' && (
                <>
                  <span>Mon</span>
                  <span>Tue</span>
                  <span>Wed</span>
                  <span>Thu</span>
                  <span>Fri</span>
                  <span>Sat</span>
                  <span>Sun</span>
                </>
              )}
              {period === 'month' && (
                <>
                  <span>Week 1</span>
                  <span>Week 2</span>
                  <span>Week 3</span>
                  <span>Week 4</span>
                </>
              )}
              {period === 'quarter' && (
                <>
                  <span>Month 1</span>
                  <span>Month 2</span>
                  <span>Month 3</span>
                </>
              )}
              {period === 'year' && (
                <>
                  <span>Q1</span>
                  <span>Q2</span>
                  <span>Q3</span>
                  <span>Q4</span>
                </>
              )}
            </div>
          </div>
        </Card>
      </div>
    </DashboardLayout>
  );
}
