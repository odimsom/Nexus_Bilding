import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { ArrowLeft, Mail, Download, CheckCircle2 } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { Badge } from '../../core/components/ui/Badge';
import { mockUser, mockFiscalDocuments } from '../../../services/mockData';
import { FiscalDocument } from '../../../types';
import { ReportGenerator } from '../../../helpers/ReportGenerator';

export function InvoiceDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [invoice, setInvoice] = useState<FiscalDocument | null>(null);

  useEffect(() => {
    // Simulate API fetch
    const found = mockFiscalDocuments.find(d => d.id === id);
    if (found) setInvoice(found);
  }, [id]);

  if (!invoice) {
    return (
      <DashboardLayout title="Invoice Details" user={mockUser}>
        <div className="flex justify-center py-20">Loading...</div>
      </DashboardLayout>
    );
  }

  return (
    <DashboardLayout title={`Invoice #${invoice.eCFNumber || 'DRAFT'}`} user={mockUser}>
      <div className="mx-auto max-w-4xl flex flex-col gap-6">
        <button 
          onClick={() => navigate(-1)}
          className="flex items-center gap-2 text-sm text-matte-text-muted hover:text-primary transition-colors w-fit"
        >
          <ArrowLeft className="w-4 h-4" /> Back to Invoices
        </button>

        <Card className="p-0 overflow-hidden">
          <div className="bg-matte-base/50 p-6 border-b border-matte-border flex flex-col md:flex-row justify-between gap-6">
            <div>
              <div className="flex items-center gap-3 mb-2">
                <h1 className="text-2xl font-bold text-matte-text">
                  Invoice {invoice.eCFNumber || 'DRAFT'}
                </h1>
                <Badge status={invoice.status}>{invoice.status}</Badge>
              </div>
              <p className="text-matte-text-muted text-sm">
                Issued on {new Date(invoice.issueDate).toLocaleDateString()}
              </p>
            </div>
            
            <div className="flex gap-2">
              <button 
                onClick={() => ReportGenerator.generateInvoicePDF(invoice)}
                className="flex items-center gap-2 px-4 py-2 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary/90 transition-colors"
              >
                <Download className="w-4 h-4" /> Download PDF
              </button>
              <button className="flex items-center gap-2 px-4 py-2 bg-matte-base border border-matte-border text-matte-text text-sm font-medium rounded-lg hover:bg-matte-border/20 transition-colors">
                <Mail className="w-4 h-4" /> Send Email
              </button>
            </div>
          </div>

          <div className="p-8 grid grid-cols-1 md:grid-cols-2 gap-12">
            <div>
              <p className="text-xs font-semibold uppercase tracking-wide text-matte-text-muted mb-4">Bill To</p>
              <h3 className="text-lg font-bold text-matte-text mb-1">{invoice.clientName}</h3>
              <p className="text-sm text-matte-text-muted mb-2">{invoice.clientTaxId}</p>
              <div className="flex items-center gap-2 text-xs text-ink-paid-text bg-ink-paid-bg/10 px-2 py-1 rounded w-fit">
                <CheckCircle2 className="w-3 h-3" /> Verified Client
              </div>
            </div>
            
            <div className="text-right">
              <p className="text-xs font-semibold uppercase tracking-wide text-matte-text-muted mb-4">Payment Details</p>
              <div className="flex flex-col gap-1 items-end">
                 <p className="text-sm text-matte-text">Due Date: <span className="font-medium">{invoice.dueDate ? new Date(invoice.dueDate).toLocaleDateString() : 'N/A'}</span></p>
                 <p className="text-sm text-matte-text">Method: <span className="font-medium">Bank Transfer</span></p>
              </div>
            </div>
          </div>

          <div className="px-8 pb-8">
            <table className="w-full text-left bg-matte-base rounded-lg overflow-hidden">
              <thead className="bg-matte-base border-b border-matte-border text-xs uppercase text-matte-text-muted">
                <tr>
                  <th className="px-6 py-3 font-semibold">Description</th>
                  <th className="px-6 py-3 font-semibold text-center">Qty</th>
                  <th className="px-6 py-3 font-semibold text-right">Price</th>
                  <th className="px-6 py-3 font-semibold text-right">Total</th>
                </tr>
              </thead>
              <tbody className="text-sm text-matte-text">
                {invoice.items.map((item, idx) => (
                  <tr key={idx} className="border-b border-matte-border last:border-0">
                    <td className="px-6 py-4">{item.description}</td>
                    <td className="px-6 py-4 text-center">{item.quantity}</td>
                    <td className="px-6 py-4 text-right">${item.unitPrice.toLocaleString()}</td>
                    <td className="px-6 py-4 text-right font-medium">${item.total.toLocaleString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="px-8 pb-8 flex justify-end">
             <div className="w-64 flex flex-col gap-3">
                <div className="flex justify-between text-sm text-matte-text-muted">
                  <span>Subtotal</span>
                  <span>${invoice.subtotal.toLocaleString()}</span>
                </div>
                <div className="flex justify-between text-sm text-matte-text-muted">
                  <span>Tax (18%)</span>
                  <span>${invoice.totalITBIS.toLocaleString()}</span>
                </div>
                <div className="h-px bg-matte-border" />
                <div className="flex justify-between text-lg font-bold text-matte-text">
                  <span>Total</span>
                  <span>${invoice.totalAmount.toLocaleString()}</span>
                </div>
             </div>
          </div>
        </Card>
      </div>
    </DashboardLayout>
  );
}
