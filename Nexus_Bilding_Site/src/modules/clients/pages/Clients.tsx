import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Search, Users, Mail, Phone, MapPin, MoreHorizontal } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Badge } from '../../core/components/ui/Badge';
import { LoadingState } from '../../core/components/ui/LoadingState';
import { clientService } from '../../../services/client.service';
import { CreateClientModal } from '../components/CreateClientModal';
import { Client } from '../../../types';

export function Clients() {
  const navigate = useNavigate();
  const [clients, setClients] = useState<Client[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState<'all' | 'active' | 'inactive'>('all');
  const [isModalOpen, setIsModalOpen] = useState(false);
  
  // Get user from local storage
  const user = JSON.parse(localStorage.getItem('user') || '{}');

  useEffect(() => {
    const fetchClients = async () => {
      try {
        const data = await clientService.getAll();
        setClients(data);
      } finally {
        setIsLoading(false);
      }
    };
    fetchClients();
  }, []);

  const filteredClients = clients.filter((client) => {
    const matchesSearch =
      client.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (client.email && client.email.toLowerCase().includes(searchTerm.toLowerCase()));
    const matchesStatus = statusFilter === 'all' || client.status === statusFilter;
    return matchesSearch && matchesStatus;
  });

  return (
    <DashboardLayout title="Clients" user={user}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        {/* Header Section */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-2.5">
            <Users className="w-10 h-10 text-primary" />
            <span className="bg-matte-base text-matte-text text-sm font-semibold px-2.5 py-1 rounded-full border border-matte-border">
              {filteredClients.length}
            </span>
          </div>
          <button
            className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-all duration-200 ease-out hover:shadow-md active:scale-[0.98]"
            onClick={() => setIsModalOpen(true)}
          >
            <Plus className="w-4 h-4" /> New Client
          </button>
        </div>

        {/* Search and Filters */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center">
          <div className="relative flex-1">
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Search className="text-matte-text-muted w-5 h-5" />
            </div>
            <input
              className="block w-full rounded-md border border-matte-border py-2.5 pl-10 pr-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
              placeholder="Search clients by name or email..."
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
          <div className="flex gap-2">
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'all'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('all')}
            >
              All
            </button>
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'active'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('active')}
            >
              Active
            </button>
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'inactive'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('inactive')}
            >
              Inactive
            </button>
          </div>
        </div>

        {/* Table Section */}
        <div className="rounded-xl border border-matte-border bg-matte-surface shadow-sm">
          {isLoading ? (
            <LoadingState message="Loading clients..." />
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full min-w-[800px] text-left">
                <thead>
                  <tr className="border-b border-matte-border bg-matte-base/50 text-sm leading-normal text-matte-text-muted">
                    <th className="px-6 py-3 font-medium">Client</th>
                    <th className="px-6 py-3 font-medium">Contact</th>
                    <th className="px-6 py-3 font-medium">Location</th>
                    <th className="px-6 py-3 font-medium">Status</th>
                    <th className="px-6 py-3 font-medium text-right">Actions</th>
                  </tr>
                </thead>
                <tbody className="text-sm text-matte-text">
                  {filteredClients.length > 0 ? (
                    filteredClients.map((client) => (
                      <tr
                        key={client.id}
                        className="border-b border-matte-border last:border-0 hover:bg-matte-base/30 transition-all duration-150 ease-out hover:shadow-sm group cursor-pointer"
                        onClick={() => navigate(`${client.id}`)}
                      >
                        <td className="px-6 py-4">
                          <div className="flex items-center gap-3">
                            <div className="h-10 w-10 flex-shrink-0 overflow-hidden rounded-full border border-matte-border bg-matte-border">
                              <div className="flex h-full w-full items-center justify-center text-sm font-bold text-matte-text">
                                {client.name.charAt(0)}
                              </div>
                            </div>
                            <div className="flex flex-col">
                              <div className="flex items-center gap-2">
                                <p className="font-medium text-matte-text group-hover:text-primary transition-colors">
                                  {client.name}
                                </p>
                              </div>
                              <div className="flex items-center gap-1.5 text-xs text-matte-text-muted font-mono">
                                <span>{client.taxId}</span>
                                {client.isCompany && (
                                  <span className="bg-matte-base px-1.5 rounded border border-matte-border/50 text-[10px] font-sans">
                                    B2B
                                  </span>
                                )}
                              </div>
                            </div>
                          </div>
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex flex-col gap-1">
                            <span className="flex items-center gap-1.5 text-sm text-matte-text">
                              <Mail className="w-3.5 h-3.5 text-matte-text-muted" />
                              {client.email || 'N/A'}
                            </span>
                            <span className="flex items-center gap-1.5 text-sm text-matte-text">
                              <Phone className="w-3.5 h-3.5 text-matte-text-muted" />
                              {client.phone || 'N/A'}
                            </span>
                          </div>
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex items-center gap-1.5">
                            <MapPin className="w-4 h-4 text-matte-text-muted" />
                            <div className="flex flex-col">
                              <span className="text-sm font-medium">{client.city || 'N/A'}</span>
                              <span className="text-xs text-matte-text-muted">{client.address || ''}</span>
                            </div>
                          </div>
                        </td>
                        <td className="px-6 py-4">
                          <Badge status={client.status}>{client.status}</Badge>
                        </td>
                        <td className="px-6 py-4 text-right">
                          <button
                            className="text-matte-text-muted hover:text-primary transition-colors"
                            onClick={(e) => {
                              e.stopPropagation();
                              console.log('More actions for', client.name);
                            }}
                          >
                            <MoreHorizontal className="w-5 h-5" />
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan={5} className="px-6 py-12 text-center">
                        <div className="flex flex-col items-center gap-3">
                          <Users className="w-12 h-12 text-matte-text-muted/50" />
                          <div>
                            <p className="text-lg font-medium text-matte-text mb-1">No clients found</p>
                            <p className="text-sm text-matte-text-muted">
                              {searchTerm || statusFilter !== 'all'
                                ? 'Try adjusting your search or filters'
                                : 'Get started by adding your first client'}
                            </p>
                          </div>
                        </div>
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
      <CreateClientModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
    </DashboardLayout>
  );
}