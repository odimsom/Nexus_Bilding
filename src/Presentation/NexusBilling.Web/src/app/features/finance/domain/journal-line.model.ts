export interface JournalLine {
  id: string;
  journalTemplateName: string;
  journalBatchName: string;
  lineNo: number;
  accountNo: string;
  postingDate: string | null;
  documentNo: string;
  description: string;
  amount: number;
  balAccountNo: string;
}
