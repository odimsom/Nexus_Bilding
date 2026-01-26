import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Edit, Plus, Mail, Phone, MapPin, MoreHorizontal, Download, Filter, FileText, CheckCircle2 } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { Badge } from '../../core/components/ui/Badge';

import { clientService } from '../../../services/client.service';
import { fiscalService } from '../../../services/fiscal.service';
import { Client } from '../../../types';
import { FiscalDocument } from '../../../types/fiscal/FiscalDocument';

export function ClientDetails() {
  const { id } = useParams();
  const [client, setClient] = useState<Client | null>(null);
  const [documents, setDocuments] = useState<FiscalDocument[]>([]);
  
  // Get user from local storage
  const user = JSON.parse(localStorage.getItem('user') || '{}');

  useEffect(() => {
    const fetchData = async () => {
      if (id) {
        try {
            const clientData = await clientService.getById(id);
            if (clientData) setClient(clientData);

            // Fetch all documents and filter by client (Optimization: Backend should support /Fiscal?clientId=X)
            const allDocs = await fiscalService.getAll();
            setDocuments(allDocs.filter(d => d.clientId === id));
        } catch (error) {
            console.error("Failed to fetch client details", error);
        }
      }
    };
    fetchData();
  }, [id]);

  if (!client) {
    return (
      <DashboardLayout title="Client Details" user={user}>
        <div className="flex h-64 items-center justify-center">
          <div className="text-matte-text-muted">Loading...</div>
        </div>
      </DashboardLayout>
    );
  }

  const clientDocuments = documents;

  return (
    <DashboardLayout title="Client Details" user={user}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        <Card className="flex flex-col gap-6 md:flex-row md:items-center md:justify-between">
          <div className="flex items-center gap-5">
            <div className="h-20 w-20 flex-shrink-0 overflow-hidden rounded-full border border-matte-border bg-matte-base flex items-center justify-center">
              <div className="text-3xl font-bold text-matte-text">
                {client.name.charAt(0)}
              </div>
            </div>
            <div>
              <div className="flex items-center gap-3">
                <h1 className="text-2xl font-bold text-matte-text">{client.name}</h1>
                <Badge status={client.status}>{client.status}</Badge>
                {client.isCompany && (
                  <span className="flex items-center gap-1 text-xs font-medium text-primary bg-primary/10 px-2 py-0.5 rounded-full">
                    B2B
                  </span>
                )}
              </div>
              <div className="flex flex-col gap-1 mt-2 sm:flex-row sm:gap-4">
                {client.email && (
                  <span className="flex items-center gap-1.5 text-sm text-matte-text-muted">
                    <Mail className="w-4 h-4" />
                    {client.email}
                  </span>
                )}
                {client.phone && (
                  <span className="flex items-center gap-1.5 text-sm text-matte-text-muted">
                    <Phone className="w-4 h-4" />
                    {client.phone}
                  </span>
                )}
              </div>
            </div>
          </div>
          <div className="flex flex-shrink-0 gap-3">
            <button className="flex items-center gap-2 rounded-lg border border-matte-border bg-matte-base px-4 py-2 text-sm font-medium text-matte-text hover:bg-matte-border/20 transition-colors shadow-sm">
              <Edit className="w-4 h-4" /> Edit
            </button>
            <button className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm">
              <Plus className="w-4 h-4" /> New Document
            </button>
          </div>
        </Card>

        <div className="grid grid-cols-1 gap-8 lg:grid-cols-3">
          <Card className="flex flex-col gap-6 h-fit">
            <div className="flex items-center justify-between">
              <h3 className="text-lg font-bold text-matte-text">Client Details</h3>
              <button className="text-matte-text-muted hover:text-primary transition-colors">
                <MoreHorizontal className="w-5 h-5" />
              </button>
            </div>

            <div className="flex flex-col gap-5">
              <div>
                <p className="text-xs font-semibold uppercase tracking-wide text-matte-text-muted mb-1.5">RNC / Tax ID</p>
                <div className="flex items-center gap-2">
                  <p className="text-sm font-medium text-matte-text font-mono">{client.taxId}</p>
                  {client.verifiedAt && (
                    <div className="flex items-center gap-1 text-[10px] text-ink-paid-text bg-ink-paid-bg px-1.5 py-0.5 rounded border border-ink-paid-border">
                      <CheckCircle2 className="w-3 h-3" /> DGII Verified
                    </div>
                  )}
                </div>
              </div>

              <div className="h-px bg-matte-border/50" />

              <div>
                <p className="text-xs font-semibold uppercase tracking-wide text-matte-text-muted mb-1.5">
                  Address
                </p>
                <div className="flex items-start gap-2">
                  <MapPin className="w-4 h-4 text-matte-text-muted mt-0.5" />
                  <p className="text-sm font-medium text-matte-text leading-relaxed">
                    {client.address || 'No address provided'}
                    {client.city && (
                      <>
                        <br />
                        {client.city}
                      </>
                    )}
                  </p>
                </div>
              </div>

              <div className="h-px bg-matte-border/50" />

              {client.notes && (
                <div className="rounded-lg bg-matte-base/50 p-4 border border-matte-border/50">
                  <p className="text-xs font-semibold uppercase tracking-wide text-matte-text-muted mb-2">
                    Private Notes
                  </p>
                  <p className="text-sm text-matte-text italic">{client.notes}</p>
                </div>
              )}
            </div>
          </Card>

          <div className="flex flex-col rounded-xl border border-matte-border bg-matte-surface shadow-sm lg:col-span-2">
            <div className="border-b border-matte-border px-6 py-4 flex items-center justify-between bg-matte-surface rounded-t-xl">
              <div className="flex items-center gap-2">
                <h3 className="text-lg font-bold text-matte-text">Fiscal Documents</h3>
                <span className="bg-matte-base text-matte-text-muted text-xs font-medium px-2 py-0.5 rounded-full border border-matte-border/50">
                  {clientDocuments.length}
                </span>
              </div>
              <div className="flex gap-2">
                <button className="flex items-center gap-1 rounded bg-matte-base border border-matte-border px-2 py-1 text-xs font-medium text-matte-text hover:bg-matte-border/20 transition-colors">
                  <Filter className="w-4 h-4" /> Filter
                </button>
                <button className="flex items-center gap-1 rounded bg-matte-base border border-matte-border px-2 py-1 text-xs font-medium text-matte-text hover:bg-matte-border/20 transition-colors">
                  <Download className="w-4 h-4" /> Export
                </button>
              </div>
            </div>

            <div className="overflow-x-auto">
              <table className="w-full min-w-[600px] text-left">
                <thead>
                  <tr className="border-b border-matte-border bg-matte-base/50 text-sm leading-normal text-matte-text-muted">
                    <th className="px-6 py-3 font-medium">Doc Number</th>
                    <th className="px-6 py-3 font-medium">Type</th>
                    <th className="px-6 py-3 font-medium">Date</th>
                    <th className="px-6 py-3 font-medium">Amount</th>
                    <th className="px-6 py-3 font-medium">Status</th>
                    <th className="px-6 py-3 font-medium text-right">Actions</th>
                  </tr>
                </thead>
                <tbody className="text-sm text-matte-text">
                  {clientDocuments.length > 0 ? (
                    clientDocuments.map((doc) => (
                      <tr
                        key={doc.id}
                        className="border-b border-matte-border last:border-0 hover:bg-matte-base/30 transition-colors group"
                      >
                        <td className="px-6 py-4 font-medium text-primary group-hover:underline cursor-pointer font-mono">
                          {doc.eCFNumber || 'DRAFT'}
                        </td>
                        <td className="px-6 py-4">
                          <span className="inline-flex items-center rounded bg-matte-base border border-matte-border px-2 py-0.5 text-xs font-medium text-matte-text">
                            {doc.documentType}
                          </span>
                        </td>
                        <td className="px-6 py-4">{new Date(doc.issueDate).toLocaleDateString('es-DO', { month: 'short', day: 'numeric', year: 'numeric' })}</td>
                        <td className="px-6 py-4 font-bold">${doc.totalAmount.toLocaleString()}</td>
                        <td className="px-6 py-4">
                          <Badge status={doc.status}>{doc.status}</Badge>
                        </td>
                        <td className="px-6 py-4 text-right">
                          <button className="text-matte-text-muted hover:text-primary transition-colors">
                            <MoreHorizontal className="w-5 h-5" />
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan={6} className="px-6 py-12 text-center">
                        <div className="flex flex-col items-center gap-3">
                          <FileText className="w-12 h-12 text-matte-text-muted/50" />
                          <p className="text-sm text-matte-text-muted">No fiscal documents found</p>
                        </div>
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
