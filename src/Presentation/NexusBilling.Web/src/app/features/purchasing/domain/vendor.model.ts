export interface Vendor {
  no: string;
  name: string;
  address: string;
  address2: string;
  city: string;
  province: string;
  country: string;
  contact: string;
  phoneNo: string;
  phoneNo2: string;
  email: string;
  webSite: string;
  rnc: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  currencyCode: string;
  creditLimit: number;
  vendorType: string;
  blocked: boolean;
}

export interface CreateVendorFormData {
  name: string;
  address: string;
  address2: string;
  city: string;
  province: string;
  country: string;
  contact: string;
  phoneNo: string;
  phoneNo2: string;
  email: string;
  webSite: string;
  rnc: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  currencyCode: string;
  creditLimit: number;
  vendorType: string;
}
