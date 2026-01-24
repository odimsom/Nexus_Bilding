import { ReactNode } from 'react';
import { useLocation } from 'react-router-dom';
import { Sidebar } from './Sidebar';
import { Header } from './Header';
import { User } from '../../../types';

interface DashboardLayoutProps {
  children: ReactNode;
  title: string;
  user?: User;
}

export function DashboardLayout({ children, title, user }: DashboardLayoutProps) {
  const location = useLocation();

  return (
    <div className="flex h-screen w-full overflow-hidden bg-matte-base text-matte-text">
      <Sidebar user={user} />

      <div className="flex flex-1 flex-col overflow-hidden">
        <Header title={title} user={user} />

        <main className="flex-1 overflow-y-auto bg-matte-base p-8">
          <div key={location.pathname} className="animate-page-in">
            {children}
          </div>
        </main>
      </div>
    </div>
  );
}
