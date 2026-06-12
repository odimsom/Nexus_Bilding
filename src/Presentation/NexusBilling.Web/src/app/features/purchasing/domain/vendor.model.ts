export interface Vendor {
  no: string;
  name: string;
  address: string;
  city: string;
  contact: string;
  blocked: boolean;
}

export interface CreateVendorFormData {
  name: string;
  address: string;
  city: string;
  contact: string;
}
