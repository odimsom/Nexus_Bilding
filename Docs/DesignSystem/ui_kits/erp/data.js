// Nexus Billing — mock data (RD market flavor; matches the domain entities)
window.NXDATA = (function () {
  const customers = [
    { no: 'C-001847', name: 'Altagracia Comercial, S.R.L.', rnc: '1-31-12345-6', city: 'Santo Domingo', contact: 'María Altagracia', salesperson: 'José Ramírez', balance: 84120.00, overdue: 12400.00, creditLimit: 250000, terms: 'NET-30', status: 'active', blocked: false },
    { no: 'C-001902', name: 'Distribuidora Caribe', rnc: '1-30-56789-1', city: 'Santiago', contact: 'Pedro Núñez', salesperson: 'Laura Méndez', balance: 208750.00, overdue: 0, creditLimit: 500000, terms: 'NET-45', status: 'active', blocked: false },
    { no: 'C-002013', name: 'Ferretería del Sur', rnc: '1-32-98765-3', city: 'San Cristóbal', contact: 'Juan de los Santos', salesperson: 'José Ramírez', balance: 1905.40, overdue: 1905.40, creditLimit: 75000, terms: 'NET-15', status: 'active', blocked: false },
    { no: 'C-002044', name: 'Supermercado Nacional', rnc: '1-01-23456-7', city: 'Santo Domingo', contact: 'Rosa Jiménez', salesperson: 'Laura Méndez', balance: 46000.00, overdue: 46000.00, creditLimit: 120000, terms: 'NET-30', status: 'overdue', blocked: false },
    { no: 'C-002101', name: 'Constructora Bávaro', rnc: '1-23-65432-2', city: 'Punta Cana', contact: 'Luis Tavárez', salesperson: 'Carla Objío', balance: 0, overdue: 0, creditLimit: 1000000, terms: 'NET-60', status: 'active', blocked: false },
    { no: 'C-002130', name: 'Colmadón La Esquina', rnc: '1-45-11111-5', city: 'La Vega', contact: 'Frank Polanco', salesperson: 'José Ramírez', balance: 12300.00, overdue: 0, creditLimit: 40000, terms: 'NET-15', status: 'blocked', blocked: true },
  ];

  const items = [
    { no: 'IT-1001', description: 'Cemento Gris Portland 42.5kg', uom: 'BAG', price: 415.00, cost: 332.00, onHand: 1240, blocked: false },
    { no: 'IT-1002', description: 'Varilla de Acero 1/2" x 30ft', uom: 'PCS', price: 690.00, cost: 548.00, onHand: 860, blocked: false },
    { no: 'IT-1140', description: 'Bloque de Concreto 6"', uom: 'PCS', price: 38.50, cost: 27.10, onHand: 9800, blocked: false },
    { no: 'IT-2210', description: 'Pintura Acrílica Blanca (Galón)', uom: 'GAL', price: 1190.00, cost: 845.00, onHand: 320, blocked: false },
    { no: 'IT-3300', description: 'Tubería PVC 4" Sanitaria', uom: 'PCS', price: 540.00, cost: 410.00, onHand: 0, blocked: true },
  ];

  const invoices = [
    { no: 'SI-2026-001847', type: 'Invoice', ncf: 'B01-00000847', vatPct: 18, customerNo: 'C-001847', billTo: 'Altagracia Comercial, S.R.L.', postingDate: '2026-06-11', dueDate: '2026-07-11', status: 'open', amount: 12400.00 },
    { no: 'SI-2026-001848', type: 'Invoice', ncf: 'B01-00000848', vatPct: 18, customerNo: 'C-001902', billTo: 'Distribuidora Caribe', postingDate: '2026-06-10', dueDate: '2026-07-25', status: 'posted', amount: 208750.00 },
    { no: 'CM-2026-000219', type: 'Credit Memo', ncf: 'B04-00000219', vatPct: 18, customerNo: 'C-002013', billTo: 'Ferretería del Sur', postingDate: '2026-06-09', dueDate: '2026-06-24', status: 'pending', amount: 1905.40 },
    { no: 'SI-2026-001851', type: 'Invoice', ncf: 'B01-00000851', vatPct: 18, customerNo: 'C-002044', billTo: 'Supermercado Nacional', postingDate: '2026-05-02', dueDate: '2026-06-01', status: 'overdue', amount: 46000.00 },
    { no: 'SI-2026-001853', type: 'Invoice', ncf: 'B01-00000853', vatPct: 18, customerNo: 'C-002101', billTo: 'Constructora Bávaro', postingDate: '2026-06-11', dueDate: '2026-08-10', status: 'draft', amount: 0 },
    { no: 'SI-2026-001844', type: 'Invoice', ncf: 'B01-00000844', vatPct: 18, customerNo: 'C-001902', billTo: 'Distribuidora Caribe', postingDate: '2026-06-04', dueDate: '2026-07-19', status: 'posted', amount: 98250.00 },
  ];

  // Lines for the focus invoice SI-2026-001847
  const invoiceLines = [
    { itemNo: 'IT-1001', description: 'Cemento Gris Portland 42.5kg', qty: 20, uom: 'BAG', price: 415.00, line: 8300.00 },
    { itemNo: 'IT-1140', description: 'Bloque de Concreto 6"', qty: 80, uom: 'PCS', price: 38.50, line: 3080.00 },
    { itemNo: 'IT-2210', description: 'Pintura Acrílica Blanca (Galón)', qty: 1, uom: 'GAL', price: 1190.00, line: 1190.00 },
  ];

  const statusTone = { open: 'info', posted: 'success', pending: 'warn', overdue: 'danger', draft: 'neutral', active: 'success', blocked: 'danger' };
  const statusLabel = { open: 'Open', posted: 'Posted', pending: 'Pending', overdue: 'Overdue', draft: 'Draft', active: 'Active', blocked: 'Blocked' };

  const aging = [
    { cap: 'Current', val: 1196132, color: 'var(--viz-1)' },
    { cap: '1–30', val: 412000, color: 'var(--viz-3)' },
    { cap: '31–60', val: 142300, color: 'var(--amber-500)' },
    { cap: '61–90', val: 58400, color: 'var(--red-500)' },
    { cap: '90+', val: 25700, color: 'var(--red-700)' },
  ];

  const money = (n) => 'RD$ ' + n.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  const moneyShort = (n) => {
    if (n >= 1e6) return 'RD$ ' + (n / 1e6).toFixed(2) + 'M';
    if (n >= 1e3) return 'RD$ ' + (n / 1e3).toFixed(1) + 'K';
    return 'RD$ ' + n.toFixed(0);
  };

  return { customers, items, invoices, invoiceLines, statusTone, statusLabel, aging, money, moneyShort };
})();
