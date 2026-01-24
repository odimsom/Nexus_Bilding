export interface User {
  id: string;

  name: string;
  email: string;
  avatarUrl?: string;

  role: 'owner' | 'seller' | 'admin';

  createdAt: string;
}
