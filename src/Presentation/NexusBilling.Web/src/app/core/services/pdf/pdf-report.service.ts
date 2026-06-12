import { Injectable } from '@angular/core';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import { TDocumentDefinitions } from 'pdfmake/interfaces';

(pdfMake as any).vfs = (pdfFonts as any).vfs ?? (pdfFonts as any).default?.vfs;

@Injectable({
  providedIn: 'root'
})
export class PdfReportService {

  constructor() { }

  /**
   * Genera y descarga un PDF basado en la definición proporcionada.
   * @param documentDefinition Definición del documento para pdfmake
   * @param fileName Nombre del archivo a descargar (opcional)
   */
  generatePdf(documentDefinition: TDocumentDefinitions, fileName: string = 'documento.pdf') {
    pdfMake.createPdf(documentDefinition).download(fileName);
  }

  /**
   * Abre el PDF en una nueva pestaña (útil para previsualización).
   */
  openPdf(documentDefinition: TDocumentDefinitions) {
    pdfMake.createPdf(documentDefinition).open();
  }

  /**
   * Genera el estilo base que asegura consistencia visual (Nexus Billing Design System)
   * utilizando colores y tipografías (simuladas, pdfmake usa Roboto por defecto a menos que se inyecten fuentes personalizadas).
   */
  getBaseStyles() {
    return {
      header: {
        fontSize: 18,
        bold: true,
        color: '#1e293b', // var(--slate-800) - text-strong
        margin: [0, 0, 0, 10]
      },
      subheader: {
        fontSize: 14,
        bold: true,
        color: '#334155', // var(--slate-700)
        margin: [0, 10, 0, 5]
      },
      tableHeader: {
        bold: true,
        fontSize: 11,
        color: '#475569', // var(--slate-600)
        fillColor: '#f8fafc', // var(--slate-50)
      },
      label: {
        fontSize: 10,
        color: '#64748b', // var(--slate-500)
        bold: true
      },
      value: {
        fontSize: 10,
        color: '#0f172a' // var(--slate-900)
      },
      totalRow: {
        bold: true,
        fontSize: 12,
        color: '#059669' // var(--emerald-600) - action color para montos positivos
      }
    };
  }
}
