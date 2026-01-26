import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { DollarSign, Clock, Users, TrendingUp, TrendingDown, FileText } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { Badge } from '../../core/components/ui/Badge';
import { LoadingState } from '../../core/components/ui/LoadingState';
import { dashboardService } from '../../../services/dashboard.service';
import { fiscalService } from '../../../services/fiscal.service';
import { DashboardMetrics, FiscalDocument } from '../../../types';

export function Dashboard() {
  const [metrics, setMetrics] = useState<DashboardMetrics | null>(null);
  const [recentDocuments, setRecentDocuments] = useState<FiscalDocument[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  
  // Get user from local storage
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  // Normalize role from backend (roles array) or fallback
  const userRole = user.roles && user.roles.length > 0 ? user.roles[0] : (user.role || 'seller');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [metricsData, documentsData] = await Promise.all([
          dashboardService.getMetrics(),
          fiscalService.getRecent(5),
        ]);
        setMetrics(metricsData);
        setRecentDocuments(documentsData);
      } catch (error) {
        console.error('Failed to fetch dashboard data:', error);
        // Could set a specific error state here if UI needs to show it
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, []);

  if (isLoading) {
    return (
      <DashboardLayout title="Dashboard Overview" user={user}>
        <div className="flex h-[80vh] items-center justify-center">
          <LoadingState message="Loading dashboard..." />
        </div>
      </DashboardLayout>
    );
  }

  const totalReceivable = metrics 
    ? metrics.accountsReceivable.over30Days + metrics.accountsReceivable.over60Days + metrics.accountsReceivable.over90Days
    : 0;

  return (
    <DashboardLayout title="Dashboard Overview" user={user}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
          <Card>
            <div className="flex items-center justify-between mb-3">
              <p className="text-sm font-medium text-matte-text-muted">Total Sales</p>
              <DollarSign className="w-5 h-5 text-matte-text-muted" />
            </div>
            <p className="text-2xl font-bold text-matte-text">
              ${metrics?.totalSales.toLocaleString() || '0'}
            </p>
            <div className="flex items-center gap-1 mt-3">
              <TrendingUp className="w-4 h-4 text-ink-paid-text" />
              <p className="text-sm font-medium text-ink-paid-text">
                +12% <span className="font-normal text-matte-text-muted">vs last month</span>
              </p>
            </div>
          </Card>

          
          {['owner', 'admin'].includes(userRole) && (
            <>
              <Card>
                <div className="flex items-center justify-between mb-3">
                  <p className="text-sm font-medium text-matte-text-muted">Accounts Receivable</p>
                  <Clock className="w-5 h-5 text-matte-text-muted" />
                </div>
                <p className="text-2xl font-bold text-matte-text">
                  ${totalReceivable.toLocaleString()}
                </p>
                <div className="flex items-center gap-1 mt-3">
                  {metrics?.accountsReceivable.over90Days ? (
                    <>
                      <TrendingDown className="w-4 h-4 text-ink-error-text" />
                      <span className="text-sm font-medium text-ink-error-text">
                        ${metrics.accountsReceivable.over90Days.toLocaleString()} over 90 days
                      </span>
                    </>
                  ) : (
                    <span className="text-sm text-matte-text-muted">All payments on track</span>
                  )}
                </div>
              </Card>

              <Card>
                <div className="flex items-center justify-between mb-3">
                  <p className="text-sm font-medium text-matte-text-muted">Projected ITBIS</p>
                  <FileText className="w-5 h-5 text-matte-text-muted" />
                </div>
                <p className="text-2xl font-bold text-matte-text">
                  ${metrics?.projectedITBIS.toLocaleString() || '0'}
                </p>
                <div className="flex items-center gap-1 mt-3">
                  <span className="text-sm text-matte-text-muted">Due next month</span>
                </div>
              </Card>
            </>
          )}

          <Card>
            <div className="flex items-center justify-between mb-3">
              <p className="text-sm font-medium text-matte-text-muted">Active Clients</p>
              <Users className="w-5 h-5 text-matte-text-muted" />
            </div>
            <p className="text-2xl font-bold text-matte-text">{metrics?.activeClients || 0}</p>
            <div className="flex items-center gap-1 mt-3">
              <span className="text-sm font-medium text-ink-paid-text">
                 {metrics?.activeClients || 0} active accounts
              </span>
            </div>
          </Card>
        </div>

        <div className="grid grid-cols-1 gap-8 lg:grid-cols-3">
          <Card className="lg:col-span-2">
            <div className="mb-6 flex items-center justify-between">
              <h3 className="text-lg font-bold text-matte-text">Revenue Overview</h3>
              <select className="rounded border-matte-border bg-matte-base px-3 py-1 text-sm text-matte-text focus:border-primary focus:ring-primary">
                <option>Last 6 Months</option>
                <option>Last Year</option>
              </select>
            </div>
            <div className="relative h-[300px] w-full">
              <svg className="h-full w-full overflow-visible" preserveAspectRatio="none" viewBox="0 0 800 300">
                <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="250" y2="250" />
                <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="190" y2="190" />
                <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="130" y2="130" />
                <line stroke="#C7C5C0" strokeDasharray="4 4" strokeWidth="1" x1="0" x2="800" y1="70" y2="70" />
                <path
                  d="M0 250 L133 180 L266 210 L400 100 L533 140 L666 80 L800 40"
                  fill="none"
                  stroke="#8c92ab"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="3"
                  vectorEffect="non-scaling-stroke"
                />
                <circle cx="133" cy="180" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
                <circle cx="266" cy="210" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
                <circle cx="400" cy="100" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
                <circle cx="533" cy="140" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
                <circle cx="666" cy="80" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
                <circle cx="800" cy="40" fill="#E6E4DF" r="4" stroke="#8c92ab" strokeWidth="2" />
              </svg>
              <div className="absolute -bottom-6 left-0 right-0 flex justify-between text-xs font-medium text-matte-text-muted">
                <span>Jan</span>
                <span>Feb</span>
                <span>Mar</span>
                <span>Apr</span>
                <span>May</span>
                <span>Jun</span>
              </div>
            </div>
          </Card>

          <Card>
            <div className="mb-4 flex items-center justify-between">
              <h3 className="text-lg font-bold text-matte-text">Recent Documents</h3>
              <Link to={`/${userRole}/invoices`} className="text-sm font-medium text-primary hover:text-primary/80">
                View All
              </Link>
            </div>
            <div className="flex flex-col gap-4">
              {recentDocuments.map((doc) => (
                <div
                  key={doc.id}
                  className="flex items-center justify-between border-b border-matte-border/50 pb-3 last:border-0 last:pb-0 hover:bg-matte-base/30 transition-all duration-150 ease-out hover:shadow-sm rounded-lg p-2 -mx-2"
                >
                  <div className="flex items-center gap-3">
                    <div className="flex size-8 items-center justify-center rounded-full bg-matte-base border border-matte-border text-xs font-bold text-matte-text">
                      {doc.clientName.charAt(0)}
                    </div>
                    <div className="flex flex-col">
                      <p className="text-sm font-medium text-matte-text">{doc.clientName}</p>
                      <p className="text-xs text-matte-text-muted font-mono">{doc.eCFNumber || 'DRAFT'}</p>
                    </div>
                  </div>
                  <div className="flex flex-col items-end">
                    <p className="text-sm font-bold text-matte-text">${doc.totalAmount.toLocaleString()}</p>
                    <Badge status={doc.status}>{doc.status}</Badge>
                  </div>
                </div>
              ))}
              {recentDocuments.length === 0 && (
                <p className="text-sm text-matte-text-muted text-center py-4">No recent documents</p>
              )}
            </div>
          </Card>
        </div>
      </div>
    </DashboardLayout>
  );
}
