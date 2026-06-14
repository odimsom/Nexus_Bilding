import { Injectable } from '@angular/core';

export interface PdfColumn { header: string; key: string; width?: number; align?: 'left' | 'right' | 'center'; }

export interface SalesOrderPdfData {
  no: string;
  documentType: string;
  customerName: string;
  customerNo: string;
  externalDocumentNo?: string | null;
  salespersonCode?: string | null;
  postingDate: string;
  dueDate: string | null;
  paymentTerms: string | null | undefined;
  paymentMethodCode: string | null | undefined;
  currencyCode: string;
  status: string;
  lines: Array<{
    lineNo: number;
    description: string;
    quantity: number;
    unitPrice: number;
    lineDiscount: number;
    amount: number;
    vat: number;
    amountIncludingVat: number;
    unitOfMeasure?: string;
    no?: string;
  }>;
  amount: number;
  amountIncludingVat: number;
  companyName?: string;
}

export interface InvoicePdfData {
  no: string;
  orderNo?: string | null;
  customerName: string;
  customerNo: string;
  billToName?: string | null;
  externalDocumentNo?: string | null;
  salespersonCode?: string | null;
  postingDate: string;
  dueDate: string | null;
  paymentTerms: string | null | undefined;
  paymentMethodCode: string | null | undefined;
  currencyCode: string;
  status: string;
  lines: Array<{
    lineNo: number;
    type?: string;
    no?: string;
    description: string;
    quantity: number;
    unitPrice: number;
    lineDiscountPct: number;
    amount: number;
    vat?: number;
    amountIncludingVat: number;
    unitOfMeasureCode?: string;
  }>;
  amount: number;
  amountIncludingVat: number;
  remainingAmount: number;
  companyName?: string;
}

export interface QuotationPdfData {
  no: string;
  customerName: string;
  customerNo: string;
  postingDate: string;
  validUntilDate: string | null;
  quotedBy: string | null;
  observations: string | null;
  externalDocumentNo?: string | null;
  paymentTermsCode?: string | null;
  paymentMethodCode?: string | null;
  currencyCode: string;
  lines: Array<{
    lineNo: number;
    lineType: 'Item' | 'Service';
    no: string;
    description: string;
    quantity: number;
    unitPrice: number;
    lineDiscountPct: number;
    amount: number;
    amountIncludingVat: number;
    vatPct: number;
    unitOfMeasure: string;
    serviceBillingType?: number | null;
    serviceStartDate?: string | null;
    serviceEndDate?: string | null;
    serviceHours?: number | null;
    resourceNo?: string | null;
  }>;
  amount: number;
  amountIncludingVat: number;
  companyName?: string;
}

export interface PurchaseOrderPdfData {
  no: string;
  vendorName: string;
  vendorNo: string;
  externalDocumentNo?: string | null;
  postingDate: string;
  dueDate?: string | null;
  paymentTermsCode?: string | null;
  currencyCode: string;
  status: string;
  lines: Array<{
    lineNo: number;
    itemNo?: string;
    description: string;
    quantity: number;
    unitPrice: number;
    lineDiscountPct: number;
    amount: number;
    amountIncludingVat: number;
    unitOfMeasure?: string;
  }>;
  amount: number;
  amountIncludingVat: number;
  companyName?: string;
}

@Injectable({ providedIn: 'root' })
export class PdfService {

  async printPurchaseOrder(data: PurchaseOrderPdfData): Promise<void> {
    const pdfMake = await import('pdfmake/build/pdfmake');
    const pdfFonts = await import('pdfmake/build/vfs_fonts');
    (pdfMake as any).default.vfs = (pdfFonts as any).default.vfs;
    const make = (pdfMake as any).default;

    const currency = data.currencyCode || 'DOP';
    const fmt = (n: number) => `${currency} ${n.toLocaleString('es-DO', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;

    const docDef = {
      pageSize: 'LETTER',
      pageMargins: [40, 50, 40, 50],
      defaultStyle: { font: 'Roboto', fontSize: 9 },
      content: [
        {
          columns: [
            {
              stack: [
                { text: data.companyName ?? 'Nexus Billing', style: 'company' },
                { text: 'nexus.server.synsetsolutions.com', color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'Orden de Compra', style: 'docType' },
                { text: data.no, style: 'docNo' },
                {
                  text: data.status === 'Released' ? 'Lanzada' : data.status === 'Open' ? 'Abierta' : data.status,
                  color: data.status === 'Released' ? '#059669' : '#6B7280',
                  fontSize: 8, bold: true
                }
              ],
              alignment: 'right'
            }
          ],
          margin: [0, 0, 0, 16]
        },
        { canvas: [{ type: 'line', x1: 0, y1: 0, x2: 515, y2: 0, lineWidth: 1, lineColor: '#E5E7EB' }], margin: [0, 0, 0, 12] },
        {
          columns: [
            {
              stack: [
                { text: 'PROVEEDOR', style: 'fieldLabel' },
                { text: data.vendorName, bold: true, fontSize: 10 },
                { text: `No. ${data.vendorNo}`, color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'FECHA EMISIÓN', style: 'fieldLabel' },
                { text: data.postingDate, style: 'monoText' },
                { text: 'VENCIMIENTO', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.dueDate ?? '—', style: 'monoText' }
              ]
            },
            {
              stack: [
                { text: 'CONDICIÓN PAGO', style: 'fieldLabel' },
                { text: data.paymentTermsCode || '—' },
                { text: 'MONEDA', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: currency }
              ]
            },
            {
              stack: [
                { text: 'REF. PROVEEDOR', style: 'fieldLabel' },
                { text: data.externalDocumentNo || '—', style: 'monoText' }
              ]
            }
          ],
          margin: [0, 0, 0, 16]
        },
        {
          table: {
            headerRows: 1,
            widths: ['auto', 'auto', '*', 'auto', 'auto', 'auto', 'auto'],
            body: [
              [
                { text: 'No.', style: 'tableHeader' },
                { text: 'Artículo', style: 'tableHeader' },
                { text: 'Descripción', style: 'tableHeader' },
                { text: 'Cant.', style: 'tableHeader', alignment: 'right' },
                { text: 'U/M', style: 'tableHeader', alignment: 'center' },
                { text: 'Precio Unit.', style: 'tableHeader', alignment: 'right' },
                { text: 'Importe', style: 'tableHeader', alignment: 'right' }
              ],
              ...data.lines.map(l => [
                { text: l.lineNo.toString(), style: 'monoText', fontSize: 8, color: '#6B7280' },
                { text: l.itemNo || '—', style: 'monoText', fontSize: 8 },
                { text: l.description, fontSize: 8 },
                { text: l.quantity.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.unitOfMeasure || 'U', alignment: 'center', fontSize: 8, color: '#6B7280' },
                { text: l.unitPrice.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.amount.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', bold: true, style: 'monoText', fontSize: 8 }
              ])
            ]
          },
          layout: {
            hLineWidth: (i: number, node: any) => i === 0 || i === 1 || i === node.table.body.length ? 1 : 0.5,
            hLineColor: () => '#E5E7EB',
            vLineWidth: () => 0,
            paddingTop: () => 6,
            paddingBottom: () => 6
          },
          margin: [0, 0, 0, 16]
        },
        {
          columns: [
            { text: '' },
            {
              width: 200,
              table: {
                widths: ['*', 'auto'],
                body: [
                  [{ text: 'Subtotal', color: '#6B7280' }, { text: fmt(data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'ITBIS (18%)', color: '#6B7280' }, { text: fmt(data.amountIncludingVat - data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'Total', bold: true, fontSize: 11 }, { text: fmt(data.amountIncludingVat), alignment: 'right', bold: true, fontSize: 11, style: 'monoText' }]
                ]
              },
              layout: {
                hLineWidth: (i: number, node: any) => i === node.table.body.length - 1 ? 1 : 0.5,
                hLineColor: () => '#E5E7EB',
                vLineWidth: () => 0,
                paddingTop: () => 5,
                paddingBottom: () => 5
              }
            }
          ]
        }
      ],
      styles: {
        company:     { fontSize: 16, bold: true, color: '#0C7156' },
        docType:     { fontSize: 11, color: '#6B7280' },
        docNo:       { fontSize: 18, bold: true, color: '#111827' },
        fieldLabel:  { fontSize: 7, color: '#9CA3AF', bold: true, margin: [0, 0, 0, 2] },
        tableHeader: { bold: true, fontSize: 8, color: '#374151', fillColor: '#F9FAFB' },
        monoText:    { font: 'Roboto', fontSize: 9 }
      }
    };

    make.createPdf(docDef).open();
  }

  async printSalesOrder(data: SalesOrderPdfData): Promise<void> {
    const pdfMake = await import('pdfmake/build/pdfmake');
    const pdfFonts = await import('pdfmake/build/vfs_fonts');
    (pdfMake as any).default.vfs = (pdfFonts as any).default.vfs;
    const make = (pdfMake as any).default;

    const currency = data.currencyCode || 'DOP';
    const fmt = (n: number) => `${currency} ${n.toLocaleString('es-DO', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;

    const docDef = {
      pageSize: 'LETTER',
      pageMargins: [40, 50, 40, 50],
      defaultStyle: { font: 'Roboto', fontSize: 9 },
      content: [
        // Header
        {
          columns: [
            {
              stack: [
                { text: data.companyName ?? 'Nexus Billing', style: 'company' },
                { text: 'nexus.server.synsetsolutions.com', color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: `Orden de Venta`, style: 'docType' },
                { text: data.no, style: 'docNo' },
                {
                  text: data.status === 'Released' ? 'Liberada' : data.status === 'Open' ? 'Abierta' : data.status,
                  color: data.status === 'Released' ? '#059669' : '#6B7280',
                  fontSize: 8, bold: true
                }
              ],
              alignment: 'right'
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Divider
        { canvas: [{ type: 'line', x1: 0, y1: 0, x2: 515, y2: 0, lineWidth: 1, lineColor: '#E5E7EB' }], margin: [0, 0, 0, 12] },
        // Info grid
        {
          columns: [
            {
              stack: [
                { text: 'CLIENTE', style: 'fieldLabel' },
                { text: data.customerName, bold: true, fontSize: 10 },
                { text: `No. ${data.customerNo}`, color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'FECHA EMISIÓN', style: 'fieldLabel' },
                { text: data.postingDate, style: 'monoText' },
                { text: 'VENCIMIENTO', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.dueDate ?? '—', style: 'monoText' }
              ]
            },
            {
              stack: [
                { text: 'CONDICIÓN PAGO', style: 'fieldLabel' },
                { text: data.paymentTerms || '—' },
                { text: 'MÉTODO DE PAGO', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.paymentMethodCode || '—' }
              ]
            },
            {
              stack: [
                { text: 'REF. EXTERNA', style: 'fieldLabel' },
                { text: data.externalDocumentNo || '—', style: 'monoText' },
                { text: 'VENDEDOR', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.salespersonCode || '—' }
              ]
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Lines table
        {
          table: {
            headerRows: 1,
            widths: ['auto', 'auto', '*', 'auto', 'auto', 'auto', 'auto', 'auto'],
            body: [
              [
                { text: 'No.', style: 'tableHeader' },
                { text: 'Producto', style: 'tableHeader' },
                { text: 'Descripción', style: 'tableHeader' },
                { text: 'Cant.', style: 'tableHeader', alignment: 'right' },
                { text: 'U/M', style: 'tableHeader', alignment: 'center' },
                { text: 'Precio Unit.', style: 'tableHeader', alignment: 'right' },
                { text: 'ITBIS %', style: 'tableHeader', alignment: 'right' },
                { text: 'Importe', style: 'tableHeader', alignment: 'right' }
              ],
              ...data.lines.map(l => [
                { text: l.lineNo.toString(), style: 'monoText', fontSize: 8, color: '#6B7280' },
                { text: l.no || '—', style: 'monoText', fontSize: 8 },
                { text: l.description, fontSize: 8 },
                { text: l.quantity.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.unitOfMeasure || 'U', alignment: 'center', fontSize: 8, color: '#6B7280' },
                { text: l.unitPrice.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.vat > 0 ? `${l.vat}%` : '—', alignment: 'right', fontSize: 8 },
                { text: l.amount.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', bold: true, style: 'monoText', fontSize: 8 }
              ])
            ]
          },
          layout: {
            hLineWidth: (i: number, node: any) => i === 0 || i === 1 || i === node.table.body.length ? 1 : 0.5,
            hLineColor: () => '#E5E7EB',
            vLineWidth: () => 0,
            paddingTop: () => 6,
            paddingBottom: () => 6
          },
          margin: [0, 0, 0, 16]
        },
        // Totals
        {
          columns: [
            { text: '' },
            {
              width: 200,
              table: {
                widths: ['*', 'auto'],
                body: [
                  [{ text: 'Subtotal', color: '#6B7280' }, { text: fmt(data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'ITBIS (18%)', color: '#6B7280' }, { text: fmt(data.amountIncludingVat - data.amount), alignment: 'right', style: 'monoText' }],
                  [
                    { text: 'Total',  bold: true, fontSize: 11 },
                    { text: fmt(data.amountIncludingVat), alignment: 'right', bold: true, fontSize: 11, style: 'monoText' }
                  ]
                ]
              },
              layout: {
                hLineWidth: (i: number, node: any) => i === node.table.body.length - 1 ? 1 : 0.5,
                hLineColor: () => '#E5E7EB',
                vLineWidth: () => 0,
                paddingTop: () => 5,
                paddingBottom: () => 5
              }
            }
          ]
        }
      ],
      styles: {
        company:     { fontSize: 16, bold: true, color: '#0C7156' }, // Nexus Emerald
        docType:     { fontSize: 11, color: '#6B7280' },
        docNo:       { fontSize: 18, bold: true, color: '#111827' },
        fieldLabel:  { fontSize: 7, color: '#9CA3AF', bold: true, margin: [0, 0, 0, 2] },
        tableHeader: { bold: true, fontSize: 8, color: '#374151', fillColor: '#F9FAFB' },
        monoText:    { font: 'Roboto', fontSize: 9 }
      }
    };

    make.createPdf(docDef).open();
  }

  async printInvoice(data: InvoicePdfData): Promise<void> {
    const pdfMake = await import('pdfmake/build/pdfmake');
    const pdfFonts = await import('pdfmake/build/vfs_fonts');
    (pdfMake as any).default.vfs = (pdfFonts as any).default.vfs;
    const make = (pdfMake as any).default;

    const currency = data.currencyCode || 'DOP';
    const fmt = (n: number) => `${currency} ${n.toLocaleString('es-DO', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;

    const getStatusTextAndColor = (status: string) => {
      switch (status) {
        case 'posted': return { text: 'Publicada', color: '#059669' };
        case 'paid': return { text: 'Pagada', color: '#6B7280' };
        case 'overdue': return { text: 'Vencida', color: '#EF4444' };
        case 'open':
        default: return { text: 'Abierta', color: '#3B82F6' };
      }
    };

    const statusObj = getStatusTextAndColor(data.status);

    const docDef = {
      pageSize: 'LETTER',
      pageMargins: [40, 50, 40, 50],
      defaultStyle: { font: 'Roboto', fontSize: 9 },
      content: [
        // Header
        {
          columns: [
            {
              stack: [
                { text: data.companyName ?? 'Nexus Billing', style: 'company' },
                { text: 'nexus.server.synsetsolutions.com', color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: `Factura`, style: 'docType' },
                { text: data.no, style: 'docNo' },
                {
                  text: statusObj.text,
                  color: statusObj.color,
                  fontSize: 8, bold: true
                }
              ],
              alignment: 'right'
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Divider
        { canvas: [{ type: 'line', x1: 0, y1: 0, x2: 515, y2: 0, lineWidth: 1, lineColor: '#E5E7EB' }], margin: [0, 0, 0, 12] },
        // Info grid
        {
          columns: [
            {
              stack: [
                { text: 'CLIENTE', style: 'fieldLabel' },
                { text: data.customerName, bold: true, fontSize: 10 },
                { text: `No. ${data.customerNo}`, color: '#6B7280', fontSize: 8 },
                { text: 'FACTURAR A', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.billToName || data.customerName, fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'FECHA EMISIÓN', style: 'fieldLabel' },
                { text: data.postingDate, style: 'monoText' },
                { text: 'VENCIMIENTO', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.dueDate ?? '—', style: 'monoText' }
              ]
            },
            {
              stack: [
                { text: 'CONDICIÓN PAGO', style: 'fieldLabel' },
                { text: data.paymentTerms || '—' },
                { text: 'MÉTODO DE PAGO', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.paymentMethodCode || '—' }
              ]
            },
            {
              stack: [
                { text: 'NO. PEDIDO ORIGEN', style: 'fieldLabel' },
                { text: data.orderNo || '—', style: 'monoText' },
                { text: 'REF. EXTERNA', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.externalDocumentNo || '—', style: 'monoText' }
              ]
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Lines table
        {
          table: {
            headerRows: 1,
            widths: ['auto', 'auto', '*', 'auto', 'auto', 'auto', 'auto', 'auto'],
            body: [
              [
                { text: 'No.', style: 'tableHeader' },
                { text: 'Producto', style: 'tableHeader' },
                { text: 'Descripción', style: 'tableHeader' },
                { text: 'Cant.', style: 'tableHeader', alignment: 'right' },
                { text: 'U/M', style: 'tableHeader', alignment: 'center' },
                { text: 'Precio Unit.', style: 'tableHeader', alignment: 'right' },
                { text: 'Desc. %', style: 'tableHeader', alignment: 'right' },
                { text: 'Importe', style: 'tableHeader', alignment: 'right' }
              ],
              ...data.lines.map(l => [
                { text: l.lineNo.toString(), style: 'monoText', fontSize: 8, color: '#6B7280' },
                { text: l.no || '—', style: 'monoText', fontSize: 8 },
                { text: l.description, fontSize: 8 },
                { text: l.quantity.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.unitOfMeasureCode || 'U', alignment: 'center', fontSize: 8, color: '#6B7280' },
                { text: l.unitPrice.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
                { text: l.lineDiscountPct > 0 ? `${l.lineDiscountPct}%` : '—', alignment: 'right', fontSize: 8 },
                { text: l.amount.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', bold: true, style: 'monoText', fontSize: 8 }
              ])
            ]
          },
          layout: {
            hLineWidth: (i: number, node: any) => i === 0 || i === 1 || i === node.table.body.length ? 1 : 0.5,
            hLineColor: () => '#E5E7EB',
            vLineWidth: () => 0,
            paddingTop: () => 6,
            paddingBottom: () => 6
          },
          margin: [0, 0, 0, 16]
        },
        // Totals
        {
          columns: [
            { text: '' },
            {
              width: 220,
              table: {
                widths: ['*', 'auto'],
                body: [
                  [{ text: 'Subtotal', color: '#6B7280' }, { text: fmt(data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'ITBIS (18%)', color: '#6B7280' }, { text: fmt(data.amountIncludingVat - data.amount), alignment: 'right', style: 'monoText' }],
                  [
                    { text: 'Total Facturado',  bold: true, fontSize: 11 },
                    { text: fmt(data.amountIncludingVat), alignment: 'right', bold: true, fontSize: 11, style: 'monoText' }
                  ],
                  [{ text: 'Pagos Aplicados', color: '#6B7280' }, { text: fmt(data.amountIncludingVat - data.remainingAmount), alignment: 'right', style: 'monoText' }],
                  [
                    { text: 'Saldo Pendiente',  bold: true, fontSize: 11, color: data.remainingAmount > 0 ? '#EF4444' : '#059669' },
                    { text: fmt(data.remainingAmount), alignment: 'right', bold: true, fontSize: 11, style: 'monoText', color: data.remainingAmount > 0 ? '#EF4444' : '#059669' }
                  ]
                ]
              },
              layout: {
                hLineWidth: (i: number, node: any) => i === node.table.body.length - 3 || i === node.table.body.length - 1 ? 1 : 0.5,
                hLineColor: () => '#E5E7EB',
                vLineWidth: () => 0,
                paddingTop: () => 5,
                paddingBottom: () => 5
              }
            }
          ]
        }
      ],
      styles: {
        company:     { fontSize: 16, bold: true, color: '#0C7156' },
        docType:     { fontSize: 11, color: '#6B7280' },
        docNo:       { fontSize: 18, bold: true, color: '#111827' },
        fieldLabel:  { fontSize: 7, color: '#9CA3AF', bold: true, margin: [0, 0, 0, 2] },
        tableHeader: { bold: true, fontSize: 8, color: '#374151', fillColor: '#F9FAFB' },
        monoText:    { font: 'Roboto', fontSize: 9 }
      }
    };

    make.createPdf(docDef).open();
  }

  async printQuotation(data: QuotationPdfData): Promise<void> {
    const pdfMake = await import('pdfmake/build/pdfmake');
    const pdfFonts = await import('pdfmake/build/vfs_fonts');
    (pdfMake as any).default.vfs = (pdfFonts as any).default.vfs;
    const make = (pdfMake as any).default;

    const currency = data.currencyCode || 'DOP';
    const fmt = (n: number) => `${currency} ${n.toLocaleString('es-DO', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`;
    const billingTypeLabel = (t: number | null | undefined) => {
      if (t === 0) return 'Tiempo estándar';
      if (t === 1) return 'Valor único';
      if (t === 2) return 'Tiempo estimado';
      return '';
    };

    const tableBody: any[][] = [
      [
        { text: 'No.', style: 'tableHeader' },
        { text: 'Tipo', style: 'tableHeader' },
        { text: 'Descripción', style: 'tableHeader' },
        { text: 'Cant.', style: 'tableHeader', alignment: 'right' },
        { text: 'U/M', style: 'tableHeader', alignment: 'center' },
        { text: 'Precio Unit.', style: 'tableHeader', alignment: 'right' },
        { text: 'Dto. %', style: 'tableHeader', alignment: 'right' },
        { text: 'ITBIS', style: 'tableHeader', alignment: 'right' },
        { text: 'Importe', style: 'tableHeader', alignment: 'right' },
      ],
      ...data.lines.map(l => {
        const descStack: any[] = [{ text: l.description, fontSize: 8 }];
        if (l.lineType === 'Service') {
          const sub: string[] = [];
          const bt = billingTypeLabel(l.serviceBillingType);
          if (bt) sub.push(bt);
          if (l.serviceStartDate || l.serviceEndDate)
            sub.push(`${l.serviceStartDate ?? '?'} → ${l.serviceEndDate ?? '?'}`);
          if (l.serviceHours) sub.push(`${l.serviceHours} hrs`);
          if (l.resourceNo) sub.push(`Recurso: ${l.resourceNo}`);
          if (sub.length)
            descStack.push({ text: sub.join(' · '), fontSize: 7, color: '#6B7280', margin: [0, 2, 0, 0] });
        }
        return [
          { text: l.no || '—', style: 'monoText', fontSize: 8, color: '#6B7280' },
          { text: l.lineType === 'Service' ? 'Servicio' : 'Artículo', fontSize: 7, color: '#6B7280' },
          { stack: descStack },
          { text: l.quantity.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
          { text: l.unitOfMeasure || '—', alignment: 'center', fontSize: 8, color: '#6B7280' },
          { text: l.unitPrice.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', style: 'monoText', fontSize: 8 },
          { text: l.lineDiscountPct > 0 ? `${l.lineDiscountPct}%` : '—', alignment: 'right', fontSize: 8 },
          { text: l.vatPct > 0 ? `${l.vatPct}%` : '—', alignment: 'right', fontSize: 8 },
          { text: l.amount.toLocaleString('es-DO', { minimumFractionDigits: 2 }), alignment: 'right', bold: true, style: 'monoText', fontSize: 8 },
        ];
      })
    ];

    const docDef: any = {
      pageSize: 'LETTER',
      pageMargins: [40, 50, 40, 60],
      defaultStyle: { font: 'Roboto', fontSize: 9 },
      footer: (currentPage: number, pageCount: number) => ({
        columns: [
          { text: 'Este documento es una cotización y no constituye una factura fiscal.', color: '#9CA3AF', fontSize: 7, margin: [40, 0, 0, 0] },
          { text: `Pág. ${currentPage} / ${pageCount}`, alignment: 'right', color: '#9CA3AF', fontSize: 7, margin: [0, 0, 40, 0] }
        ],
        margin: [0, 8, 0, 0]
      }),
      content: [
        // Header
        {
          columns: [
            {
              stack: [
                { text: data.companyName ?? 'Nexus Billing', style: 'company' },
                { text: 'nexus.server.synsetsolutions.com', color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'COTIZACIÓN', style: 'docType' },
                { text: data.no, style: 'docNo' },
                data.validUntilDate
                  ? { text: `Válida hasta: ${data.validUntilDate}`, color: '#DC2626', fontSize: 8, bold: true }
                  : { text: '' }
              ],
              alignment: 'right'
            }
          ],
          margin: [0, 0, 0, 16]
        },
        { canvas: [{ type: 'line', x1: 0, y1: 0, x2: 515, y2: 0, lineWidth: 1.5, lineColor: '#0C7156' }], margin: [0, 0, 0, 12] },
        // Info grid
        {
          columns: [
            {
              stack: [
                { text: 'CLIENTE', style: 'fieldLabel' },
                { text: data.customerName, bold: true, fontSize: 10 },
                { text: `No. ${data.customerNo}`, color: '#6B7280', fontSize: 8 }
              ]
            },
            {
              stack: [
                { text: 'FECHA EMISIÓN', style: 'fieldLabel' },
                { text: data.postingDate, style: 'monoText' },
                { text: 'VÁLIDA HASTA', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                {
                  text: data.validUntilDate ?? 'Sin vencimiento',
                  style: 'monoText',
                  color: data.validUntilDate ? '#DC2626' : '#9CA3AF'
                }
              ]
            },
            {
              stack: [
                { text: 'CONDICIÓN PAGO', style: 'fieldLabel' },
                { text: data.paymentTermsCode || '—' },
                { text: 'REF. EXTERNA', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: data.externalDocumentNo || '—', style: 'monoText' }
              ]
            },
            {
              stack: [
                { text: 'COTIZÓ', style: 'fieldLabel' },
                { text: data.quotedBy || '—' },
                { text: 'MONEDA', style: 'fieldLabel', margin: [0, 6, 0, 0] },
                { text: currency, style: 'monoText' }
              ]
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Lines table
        {
          table: { headerRows: 1, widths: ['auto', 'auto', '*', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto'], body: tableBody },
          layout: {
            hLineWidth: (i: number, node: any) => i === 0 || i === 1 || i === node.table.body.length ? 1 : 0.5,
            hLineColor: (i: number) => i === 0 ? '#0C7156' : '#E5E7EB',
            vLineWidth: () => 0,
            paddingTop: () => 6,
            paddingBottom: () => 6
          },
          margin: [0, 0, 0, 16]
        },
        // Totals
        {
          columns: [
            { text: '' },
            {
              width: 220,
              table: {
                widths: ['*', 'auto'],
                body: [
                  [{ text: 'Subtotal', color: '#6B7280' }, { text: fmt(data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'ITBIS (18%)', color: '#6B7280' }, { text: fmt(data.amountIncludingVat - data.amount), alignment: 'right', style: 'monoText' }],
                  [{ text: 'TOTAL', bold: true, fontSize: 11, color: '#0C7156' }, { text: fmt(data.amountIncludingVat), alignment: 'right', bold: true, fontSize: 11, style: 'monoText', color: '#0C7156' }]
                ]
              },
              layout: {
                hLineWidth: (i: number, node: any) => i === node.table.body.length - 1 ? 1.5 : 0.5,
                hLineColor: (i: number, node: any) => i === node.table.body.length - 1 ? '#0C7156' : '#E5E7EB',
                vLineWidth: () => 0,
                paddingTop: () => 5,
                paddingBottom: () => 5
              }
            }
          ],
          margin: [0, 0, 0, 16]
        },
        // Observations
        ...(data.observations ? [{
          stack: [
            { canvas: [{ type: 'line', x1: 0, y1: 0, x2: 515, y2: 0, lineWidth: 0.5, lineColor: '#E5E7EB' }], margin: [0, 0, 0, 8] },
            { text: 'OBSERVACIONES', style: 'fieldLabel' },
            { text: data.observations, fontSize: 8, color: '#374151', margin: [0, 4, 0, 0] }
          ]
        }] : [])
      ],
      styles: {
        company:     { fontSize: 16, bold: true, color: '#0C7156' },
        docType:     { fontSize: 11, color: '#0C7156', bold: true, letterSpacing: 2 },
        docNo:       { fontSize: 18, bold: true, color: '#111827' },
        fieldLabel:  { fontSize: 7, color: '#9CA3AF', bold: true, margin: [0, 0, 0, 2] },
        tableHeader: { bold: true, fontSize: 8, color: '#374151', fillColor: '#F0FDF4' },
        monoText:    { font: 'Roboto', fontSize: 9 }
      }
    };

    make.createPdf(docDef).open();
  }
}
