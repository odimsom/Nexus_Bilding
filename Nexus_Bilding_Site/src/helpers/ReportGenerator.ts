import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import * as XLSX from 'xlsx';
import { FiscalDocument, FiscalDocumentItem } from '../types';

/**
 * Helper service for generating reports (PDF/Excel) from invoice data.
 */

export const ReportGenerator = {
  generateInvoicePDF: async (invoice: FiscalDocument) => {
    try {
      const doc = new jsPDF();
      
      // Header
      doc.setFontSize(20);
      doc.setTextColor(40, 40, 40);
      doc.text("NEXUS BILLING", 14, 22);
      
      doc.setFontSize(10);
      doc.setTextColor(100);
      doc.text(`Comprobante Fiscal: ${invoice.eCFNumber || 'BORRADOR'}`, 14, 30);
      doc.text(`Fecha: ${invoice.issueDate}`, 14, 35);
      doc.text(`Cliente: ${invoice.clientName}`, 14, 40);
      if (invoice.clientTaxId) {
        doc.text(`RNC/Cédula: ${invoice.clientTaxId}`, 14, 45);
      }

      // Table
      const tableColumn = ["Descripción", "Cant", "Precio", "ITBIS", "Total"];
      const tableRows: any[] = [];

      invoice.items.forEach((item: FiscalDocumentItem) => {
        const itemData = [
          item.description,
          item.quantity,
          `$${item.unitPrice.toFixed(2)}`,
          `$${item.itbisAmount.toFixed(2)}`,
          `$${item.total.toFixed(2)}`,
        ];
        tableRows.push(itemData);
      });

      autoTable(doc, {
        head: [tableColumn],
        body: tableRows,
        startY: 55,
        theme: 'grid',
        styles: { fontSize: 9 },
        headStyles: { fillColor: [20, 20, 20] } // Dark header matching branding
      });

      // Totals
      // @ts-ignore - access finalY from last autoTable call
      const finalY = (doc as any).lastAutoTable.finalY + 10;
      
      doc.setFontSize(10);
      doc.setTextColor(0);
      doc.text(`Subtotal: $${invoice.subtotal.toFixed(2)}`, 140, finalY);
      doc.text(`ITBIS: $${invoice.totalITBIS.toFixed(2)}`, 140, finalY + 5);
      
      doc.setFontSize(12);
      doc.setFont("helvetica", "bold");
      doc.text(`Total: $${invoice.totalAmount.toFixed(2)}`, 140, finalY + 12);

      // Save
      doc.save(`Factura_${invoice.eCFNumber || invoice.id}.pdf`);
      return true;
    } catch (error) {
      console.error('Error generating PDF:', error);
      return false;
    }
  },

  generateInvoicesExcel: async (invoices: FiscalDocument[]) => {
    try {
      // Prepare data for Excel
      const data = invoices.map(inv => ({
        ID: inv.id,
        Tipo: inv.documentType,
        NCF: inv.eCFNumber || 'N/A',
        Cliente: inv.clientName,
        RNC: inv.clientTaxId || 'N/A',
        Fecha: inv.issueDate,
        Estado: inv.status,
        Subtotal: inv.subtotal,
        ITBIS: inv.totalITBIS,
        Total: inv.totalAmount
      }));

      // Create Worksheet
      const ws = XLSX.utils.json_to_sheet(data);
      
      // Create Workbook
      const wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, "Facturas");
      
      // Generate file
      XLSX.writeFile(wb, "Reporte_Facturas.xlsx");
      return true;
    } catch (error) {
      console.error('Error generating Excel:', error);
      return false;
    }
  }
};
