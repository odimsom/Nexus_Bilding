import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

export interface ExcelColumn {
  header: string;
  key: string;
  width?: number;
  numFmt?: string;
  alignment?: { horizontal?: string };
}

export interface ExcelExportOptions {
  filename: string;
  title: string;
  subtitle?: string;
  sheetName?: string;
  columns: ExcelColumn[];
  data: any[];
}

@Injectable({ providedIn: 'root' })
export class ExcelExportService {

  async exportAsExcel(options: ExcelExportOptions): Promise<void> {
    const wb = XLSX.utils.book_new();
    const dateStr = new Intl.DateTimeFormat('es-DO', { dateStyle: 'long', timeStyle: 'short' }).format(new Date());
    const subtitle = options.subtitle
      ? `${options.subtitle}  •  Generado: ${dateStr}`
      : `Generado: ${dateStr}`;

    const sheetName = (options.sheetName ?? options.filename).slice(0, 31);

    // Build rows: title row, subtitle row, blank, header row, data rows
    const titleRow = [options.title];
    const subtitleRow = [subtitle];
    const blankRow: string[] = [];
    const headerRow = options.columns.map(c => c.header);
    const dataRows = options.data.map(row =>
      options.columns.map(col => {
        const val = row[col.key];
        return val === undefined || val === null ? '' : val;
      })
    );

    const allRows = [titleRow, subtitleRow, blankRow, headerRow, ...dataRows];
    const ws = XLSX.utils.aoa_to_sheet(allRows);

    const totalCols = options.columns.length;

    // Column widths
    ws['!cols'] = options.columns.map(c => ({ wch: c.width ?? 18 }));

    // Merge title across all columns
    ws['!merges'] = [
      { s: { r: 0, c: 0 }, e: { r: 0, c: totalCols - 1 } },
      { s: { r: 1, c: 0 }, e: { r: 1, c: totalCols - 1 } },
    ];

    // Style title cell (bold, larger)
    const titleCellAddr = XLSX.utils.encode_cell({ r: 0, c: 0 });
    if (ws[titleCellAddr]) {
      ws[titleCellAddr].s = {
        font: { bold: true, sz: 14, color: { rgb: '1A1A2E' } },
        fill: { fgColor: { rgb: '0D9488' } },
        alignment: { horizontal: 'left', vertical: 'center' },
      };
    }

    // Style subtitle cell
    const subCellAddr = XLSX.utils.encode_cell({ r: 1, c: 0 });
    if (ws[subCellAddr]) {
      ws[subCellAddr].s = {
        font: { sz: 9, color: { rgb: '6B7280' }, italic: true },
        alignment: { horizontal: 'left' },
      };
    }

    // Style header row (row index 3)
    for (let c = 0; c < totalCols; c++) {
      const addr = XLSX.utils.encode_cell({ r: 3, c });
      if (ws[addr]) {
        ws[addr].s = {
          font: { bold: true, sz: 10, color: { rgb: 'FFFFFF' } },
          fill: { fgColor: { rgb: '1E293B' } },
          alignment: { horizontal: 'center', vertical: 'center' },
          border: {
            bottom: { style: 'thin', color: { rgb: '0D9488' } },
          },
        };
      }
    }

    // Style data rows with alternating background
    const dataStartRow = 4;
    for (let r = 0; r < dataRows.length; r++) {
      const isEven = r % 2 === 0;
      const fillColor = isEven ? 'F8FAFC' : 'FFFFFF';
      for (let c = 0; c < totalCols; c++) {
        const addr = XLSX.utils.encode_cell({ r: dataStartRow + r, c });
        if (ws[addr]) {
          ws[addr].s = {
            fill: { fgColor: { rgb: fillColor } },
            font: { sz: 10 },
            alignment: { vertical: 'center' },
          };
        }
      }
    }

    // Freeze top 4 rows (title, subtitle, blank, header)
    ws['!freeze'] = { xSplit: 0, ySplit: 4 };

    // Row heights
    ws['!rows'] = [
      { hpt: 24 }, // title
      { hpt: 14 }, // subtitle
      { hpt: 6  }, // blank
      { hpt: 20 }, // header
    ];

    XLSX.utils.book_append_sheet(wb, ws, sheetName);

    const wbOut = XLSX.write(wb, { bookType: 'xlsx', type: 'array', cellStyles: true });
    const blob = new Blob([wbOut], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `${options.filename}.xlsx`;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
  }
}
