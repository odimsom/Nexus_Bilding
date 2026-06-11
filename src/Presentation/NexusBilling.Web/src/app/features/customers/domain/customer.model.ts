/**
 * Customer — frontend model
 * Maps: NexusBilling.Core.Domain.Sales.Entities.Customer
 *
 * Fields: No, Name, Address, City, Contact, Blocked
 * Extended with UI-relevant fields from Customer EF Configuration
 */
export interface Customer {
  no: string;
  name: string;
  address: string;
  city: string;
  contact: string;
  blocked: boolean;
  phoneNo?: string;
  email?: string;
  creditLimit?: number;
  balance?: number;
  balanceDue?: number;
  customerPostingGroup?: string;
  paymentTermsCode?: string;
  salespersonCode?: string;
  currencyCode?: string;
  countryRegionCode?: string;
  vatRegistrationNo?: string;
}

export interface CustomerFilter {
  search?: string;
  blocked?: boolean | null;
  city?: string;
  salespersonCode?: string;
}

export type CustomerSortField = 'no' | 'name' | 'city' | 'balance' | 'balanceDue';
