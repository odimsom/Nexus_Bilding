export interface Customer {
  no: string;
  name: string;
  address: string;
  city: string;
  contact: string;
  blocked: boolean;
  phoneNo: string;
  email: string;
  creditLimit: number;
  balance: number;
  balanceDue: number;
  vatRegistrationNo: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  currencyCode: string;
  customerPostingGroup: string;
  countryRegionCode: string;
}

export interface CustomerFilter {
  search?: string;
  blocked?: boolean | null;
  city?: string;
  salespersonCode?: string;
}

export type CustomerSortField = 'no' | 'name' | 'city' | 'balance' | 'balanceDue';
