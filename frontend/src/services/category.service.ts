import { apiClient } from './apiClient';

export interface Category {
  id: string;
  name: string;
  description: string;
  isActive: boolean;
  createdDate: string;
  updatedDate: string | null;
}

export const categoryService = {
  getAll: () => apiClient.get<Category[]>('/Category'),
};
