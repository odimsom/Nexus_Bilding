import { Injectable } from '@angular/core';
import * as ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';

export interface ExcelColumn {
  header: string;
  key: string;
  width?: number;
  numFmt?: string;
  alignment?: Partial<ExcelJS.Alignment>;
}

export interface ExcelExportOptions {
  filename: string;
  title: string;
  subtitle?: string;
  sheetName?: string;
  columns: ExcelColumn[];
  data: any[];
}

@Injectable({
  providedIn: 'root'
})
export class ExcelExportService {
  constructor() {}

  async exportAsExcel(options: ExcelExportOptions): Promise<void> {
    const workbook = new ExcelJS.Workbook();
    workbook.creator = 'Nexus Billing';
    workbook.lastModifiedBy = 'Nexus Billing';
    workbook.created = new Date();
    workbook.modified = new Date();

    const sheetName = options.sheetName || 'Hoja1';
    const worksheet = workbook.addWorksheet(sheetName, {
      properties: { tabColor: { argb: 'FF004B87' } },
      views: [{ showGridLines: false }]
    });

    let currentRow = 1;

    // 1. Título
    const titleCell = worksheet.getCell(`A${currentRow}`);
    titleCell.value = options.title;
    titleCell.font = { name: 'Arial', size: 16, bold: true, color: { argb: 'FF003366' } };
    titleCell.alignment = { vertical: 'middle', horizontal: 'left' };
    currentRow++;

    // 2. Subtítulo (ej. Fecha de generación)
    const dateStr = new Intl.DateTimeFormat('es-DO', { dateStyle: 'full', timeStyle: 'short' }).format(new Date());
    const subtitleCell = worksheet.getCell(`A${currentRow}`);
    subtitleCell.value = options.subtitle ? `${options.subtitle} - Generado el: ${dateStr}` : `Generado el: ${dateStr}`;
    subtitleCell.font = { name: 'Arial', size: 10, italic: true, color: { argb: 'FF666666' } };
    subtitleCell.alignment = { vertical: 'middle', horizontal: 'left' };
    currentRow += 2; // Dejar una fila en blanco

    // 3. Encabezados de la Tabla
    const headerRow = worksheet.getRow(currentRow);
    options.columns.forEach((col, index) => {
      const cell = headerRow.getCell(index + 1);
      cell.value = col.header;
      cell.font = { name: 'Arial', size: 11, bold: true, color: { argb: 'FFFFFFFF' } };
      cell.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: 'FF004B87' } // Nexus Blue
      };
      cell.alignment = { vertical: 'middle', horizontal: col.alignment?.horizontal || 'left', wrapText: true };
      cell.border = {
        top: { style: 'thin', color: { argb: 'FF004B87' } },
        left: { style: 'thin', color: { argb: 'FFFFFFFF' } },
        bottom: { style: 'thin', color: { argb: 'FF004B87' } },
        right: { style: 'thin', color: { argb: 'FFFFFFFF' } }
      };
      
      // Establecer ancho de columna
      const wsColumn = worksheet.getColumn(index + 1);
      wsColumn.width = col.width || 20;
    });
    headerRow.height = 25;
    
    // Activar Autofiltro
    const lastColLetter = this.getColumnLetter(options.columns.length);
    worksheet.autoFilter = `A${currentRow}:${lastColLetter}${currentRow}`;
    
    currentRow++;

    // 4. Datos
    options.data.forEach((rowData, rowIndex) => {
      const row = worksheet.getRow(currentRow);
      const isAlternate = rowIndex % 2 !== 0;
      
      options.columns.forEach((col, colIndex) => {
        const cell = row.getCell(colIndex + 1);
        const val = rowData[col.key];
        cell.value = val === undefined || val === null ? '' : val;
        
        cell.font = { name: 'Arial', size: 10, color: { argb: 'FF333333' } };
        cell.alignment = { vertical: 'middle', horizontal: col.alignment?.horizontal || 'left' };
        
        if (col.numFmt) {
          cell.numFmt = col.numFmt;
        }

        // Color de cebra sutil
        if (isAlternate) {
          cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: { argb: 'FFF9FAFB' }
          };
        }

        // Bordes suaves
        cell.border = {
          bottom: { style: 'hair', color: { argb: 'FFE5E7EB' } }
        };
      });
      
      row.height = 20;
      currentRow++;
    });

    // 5. Descarga
    const buffer = await workbook.xlsx.writeBuffer();
    const blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    saveAs(blob, `${options.filename}.xlsx`);
  }

  private getColumnLetter(colIndex: number): string {
    let temp = colIndex;
    let letter = '';
    while (temp > 0) {
      let modulo = (temp - 1) % 26;
      letter = String.fromCharCode(65 + modulo) + letter;
      temp = (temp - modulo) / 26;
    }
    return letter;
  }
}
