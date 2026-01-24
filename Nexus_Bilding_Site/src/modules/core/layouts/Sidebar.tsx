import { Link, useLocation } from 'react-router-dom';
import { Database, LayoutDashboard, FileText, Users, Package, BarChart3, Settings, LogOut } from 'lucide-react';
import { User } from '../../../types';

const menuItems = [
  { path: '/dashboard', icon: LayoutDashboard, label: 'Dashboard', allowedRoles: ['seller', 'owner', 'admin'] },
  { path: '/invoices', icon: FileText, label: 'Invoices', allowedRoles: ['seller', 'owner', 'admin'] },
  { path: '/clients', icon: Users, label: 'Clients', allowedRoles: ['seller', 'owner', 'admin'] },
  { path: '/inventory', icon: Package, label: 'Inventory', allowedRoles: ['seller', 'owner', 'admin'] },
  { path: '/reports', icon: BarChart3, label: 'Reports', allowedRoles: ['owner', 'admin'] },
  { path: '/settings', icon: Settings, label: 'Settings', allowedRoles: ['owner', 'admin'] },
];

interface SidebarProps {
  user?: User; // Will inherit from parent or use mock default if needed, but prop is better
}

export function Sidebar({ user }: SidebarProps) {
  const location = useLocation();
  const userRole = user?.role || 'seller'; // Default to restricted if unknown

  const filteredItems = menuItems.filter(item => item.allowedRoles.includes(userRole));

  return (
    <aside className="flex w-64 flex-col border-r border-matte-border bg-matte-surface">
      <div className="flex h-20 items-center gap-3 border-b border-matte-border px-6">
        <div className="flex size-10 items-center justify-center rounded-lg bg-primary/20 text-primary">
          <Database className="w-6 h-6" />
        </div>
        <div className="flex flex-col">
          <h1 className="text-base font-bold text-matte-text">Nexus Billing</h1>
          <p className="text-xs text-matte-text-muted">v2.1.0</p>
        </div>
      </div>

      <nav className="flex flex-1 flex-col gap-1 overflow-y-auto px-4 py-6">
        {filteredItems.map((item) => {
          const Icon = item.icon;
          const targetPath = `/${userRole}${item.path}`;
          const isActive = location.pathname === targetPath;

          return (
            <Link
              key={item.path}
              to={`/${userRole}${item.path}`}
              className={`flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-all duration-200 ease-out hover:shadow-sm ${
                isActive
                  ? 'bg-primary/15 text-primary shadow-sm'
                  : 'text-matte-text hover:bg-black/5 hover:text-primary'
              }`}
            >
              <Icon className="w-5 h-5" />
              <span>{item.label}</span>
            </Link>
          );
        })}
      </nav>

      <div className="border-t border-matte-border p-4">
        <button className="flex w-full items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium text-matte-text hover:bg-black/5 hover:text-ink-error-text transition-all duration-200 ease-out hover:shadow-sm">
          <LogOut className="w-5 h-5" />
          <span>Log Out</span>
        </button>
      </div>
    </aside>
  );
}
