import 'reflect-metadata';
import { Expose, plainToInstance } from 'class-transformer';
import { apiClient, type ApiResponse } from './apiClient';

export class Product {

  id!: string;

  @Expose({name: "name"})
  ad!: string;

  unitPrice!: number;

  unitInStock!: number;

  discontinued!: boolean;

  isActive!: boolean;

  categoryID!: string | null;

  @Expose({name: "categoryName"})
  kadi!: string | null;
}

export const ProductService = {
  getAll: async (): Promise<ApiResponse<Product[]>> => {
    const result = await apiClient.get<unknown[]>('/Product/with-category');
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const products = plainToInstance(Product, result.data);
    return { data: products, error: null };
  },
};


