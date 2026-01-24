import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Search, Bell, MessageSquare } from 'lucide-react';
import { User } from '../../../types';
import { NotificationsModal } from '../../users/pages/modals/Notifications';

interface HeaderProps {
  title: string;
  user?: User;
}

export function Header({ title, user }: HeaderProps) {
  const navigate = useNavigate();
  const [showNotifications, setShowNotifications] = useState(false);

  return (
    <>
      <header className="flex h-20 items-center justify-between border-b border-matte-border bg-matte-base px-8 py-3">
        <h2 className="text-2xl font-bold tracking-tight text-matte-text">{title}</h2>

        <div className="flex items-center gap-6">
          <div className="relative flex h-10 w-64 items-center overflow-hidden rounded-lg border border-matte-border bg-matte-surface">
            <div className="flex h-full items-center justify-center pl-3 text-matte-text-muted">
              <Search className="w-5 h-5" />
            </div>
            <input
              className="h-full w-full border-none bg-transparent px-3 text-sm text-matte-text placeholder-matte-text-muted focus:ring-0"
              placeholder="Search invoices..."
            />
          </div>

          <div className="flex items-center gap-3">
            <button 
              onClick={() => setShowNotifications(!showNotifications)}
              className="flex size-10 items-center justify-center rounded-lg border border-matte-border bg-matte-surface text-matte-text hover:bg-matte-border/30 transition-all duration-200 ease-out hover:shadow-sm hover:-translate-y-0.5"
            >
              <Bell className="w-5 h-5" />
            </button>

            <button className="flex size-10 items-center justify-center rounded-lg border border-matte-border bg-matte-surface text-matte-text hover:bg-matte-border/30 transition-all duration-200 ease-out hover:shadow-sm hover:-translate-y-0.5">
              <MessageSquare className="w-5 h-5" />
            </button>

            <div 
              className="h-10 w-10 overflow-hidden rounded-full border border-matte-border bg-matte-surface cursor-pointer hover:ring-2 hover:ring-primary/20 transition-all"
              onClick={() => navigate(`/${user?.role || 'seller'}/profile`)}
            >
              {user?.avatarUrl ? (
                <img
                  alt={user.name}
                  className="h-full w-full object-cover"
                  src={user.avatarUrl}
                />
              ) : (
                <div className="flex h-full w-full items-center justify-center bg-matte-border text-matte-text text-sm font-bold">
                  {user?.name?.charAt(0) || 'U'}
                </div>
              )}
            </div>
          </div>
        </div>
      </header>

      <NotificationsModal 
        isOpen={showNotifications} 
        onClose={() => setShowNotifications(false)} 
      />
    </>
  );
}
