import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Search, FileText, MoreHorizontal, Download } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Badge } from '../../core/components/ui/Badge';
import { LoadingState } from '../../core/components/ui/LoadingState';
import { fiscalService } from '../../../services/fiscal.service';
import { FiscalDocument } from '../../../types';

import { ReportGenerator } from '../../../helpers/ReportGenerator';
import { CreateInvoiceModal } from '../components/CreateInvoiceModal';

export function Invoices() {
  const navigate = useNavigate();
  const [documents, setDocuments] = useState<FiscalDocument[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState<'all' | 'draft' | 'issued' | 'paid'>('all');
  const [isModalOpen, setIsModalOpen] = useState(false);
  
  // Get user from local storage
  const user = JSON.parse(localStorage.getItem('user') || '{}');


  const handleExport = async () => {
    // For MVP, we export all filtered documents to Excel
    // In future, could open a modal to select PDF vs Excel and date range
    await ReportGenerator.generateInvoicesExcel(documents);
  };

  useEffect(() => {
    const fetchDocuments = async () => {
      try {
        const data = await fiscalService.getAll();
        setDocuments(data);
      } catch (error) {
        console.error('Failed to fetch documents:', error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchDocuments();
  }, []);

  const filteredDocuments = documents.filter((doc) => {
    const matchesSearch =
      doc.clientName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (doc.eCFNumber && doc.eCFNumber.toLowerCase().includes(searchTerm.toLowerCase()));
      // Add more search criteria if needed
    
    // Simple mapping for filter to status (can be improved)
    const matchesStatus = statusFilter === 'all' || doc.status === statusFilter || (statusFilter === 'issued' && doc.status === 'sent');
    return matchesSearch && matchesStatus;
  });

  return (
    <DashboardLayout title="Fiscal Documents" user={user}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        {/* Header Section */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-2.5">
            <FileText className="w-10 h-10 text-primary" />
            <span className="bg-matte-base text-matte-text text-sm font-semibold px-2.5 py-1 rounded-full border border-matte-border">
              {filteredDocuments.length}
            </span>
          </div>
          <div className="flex gap-3">
            <button 
              onClick={handleExport}
              className="flex items-center gap-2 rounded-lg border border-matte-border bg-matte-base px-4 py-2 text-sm font-medium text-matte-text hover:bg-matte-border/20 transition-all duration-200 ease-out hover:shadow-md active:scale-[0.98]"
            >
              <Download className="w-4 h-4" /> Export
            </button>
            <button
              className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-all duration-200 ease-out hover:shadow-md active:scale-[0.98]"
              onClick={() => setIsModalOpen(true)}
            >
              <Plus className="w-4 h-4" /> New Document
            </button>
          </div>
        </div>

        {/* Search and Filters */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center">
          <div className="relative flex-1">
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Search className="text-matte-text-muted w-5 h-5" />
            </div>
            <input
              className="block w-full rounded-md border border-matte-border py-2.5 pl-10 pr-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
              placeholder="Search by client or NCF/eCF number..."
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
                statusFilter === 'draft'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('draft')}
            >
              Drafts
            </button>
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'issued'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('issued')}
            >
              Issued
            </button>
          </div>
        </div>

        {/* Table Section */}
        <div className="rounded-xl border border-matte-border bg-matte-surface shadow-sm">
          {isLoading ? (
            <LoadingState message="Loading documents..." />
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full min-w-[900px] text-left">
                <thead>
                  <tr className="border-b border-matte-border bg-matte-base/50 text-sm leading-normal text-matte-text-muted">
                    <th className="px-6 py-3 font-medium">Document ID</th>
                    <th className="px-6 py-3 font-medium">Type</th>
                    <th className="px-6 py-3 font-medium">Client</th>
                    <th className="px-6 py-3 font-medium">Date</th>
                    <th className="px-6 py-3 font-medium">Amount</th>
                    <th className="px-6 py-3 font-medium">Status</th>
                    <th className="px-6 py-3 font-medium text-right">Actions</th>
                  </tr>
                </thead>
                <tbody className="text-sm text-matte-text">
                  {filteredDocuments.length > 0 ? (
                    filteredDocuments.map((doc) => (
                      <tr
                        key={doc.id}
                        className="border-b border-matte-border last:border-0 hover:bg-matte-base/30 transition-all duration-150 ease-out hover:shadow-sm group cursor-pointer"
                        onClick={() => navigate(`${doc.id}`)}
                      >
                        <td className="px-6 py-4 font-mono font-medium text-primary">
                          {doc.eCFNumber || 'DRAFT'}
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex flex-col">
                            <span className="font-medium">{doc.documentType}</span>
                          </div>
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex flex-col">
                            <span className="font-medium text-matte-text">{doc.clientName}</span>
                            <span className="text-xs text-matte-text-muted font-mono">{doc.clientTaxId}</span>
                          </div>
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex flex-col">
                            <span>{new Date(doc.issueDate).toLocaleDateString('es-DO', { month: 'short', day: 'numeric', year: 'numeric' })}</span>
                            <span className="text-xs text-matte-text-muted">Due: {new Date(doc.dueDate || doc.issueDate).toLocaleDateString('es-DO', { month: 'short', day: 'numeric' })}</span>
                          </div>
                        </td>
                        <td className="px-6 py-4 font-bold">
                          ${doc.totalAmount.toLocaleString()}
                        </td>
                        <td className="px-6 py-4">
                          <Badge status={doc.status}>{doc.status}</Badge>
                        </td>
                        <td className="px-6 py-4 text-right">
                          <button
                            className="text-matte-text-muted hover:text-primary transition-colors"
                            onClick={(e) => {
                              e.stopPropagation();
                              console.log('More actions');
                            }}
                          >
                            <MoreHorizontal className="w-5 h-5" />
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan={7} className="px-6 py-12 text-center">
                        <div className="flex flex-col items-center gap-3">
                          <FileText className="w-12 h-12 text-matte-text-muted/50" />
                          <div>
                            <p className="text-lg font-medium text-matte-text mb-1">No documents found</p>
                            <p className="text-sm text-matte-text-muted">
                              {searchTerm ? 'Try adjusting your search' : 'Create your first fiscal document'}
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
      <CreateInvoiceModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
    </DashboardLayout>
  );
}

