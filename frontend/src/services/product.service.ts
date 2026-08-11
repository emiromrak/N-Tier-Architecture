import 'reflect-metadata';
import { Expose, plainToInstance } from 'class-transformer';
import { apiClient, type ApiResponse } from './apiClient';

export class Product {
  @Expose()
  id!: string;

  @Expose({ name: 'name' })
  urun_ad!: string;

  @Expose()
  unitPrice!: number;

  @Expose()
  unitInStock!: number;

  @Expose()
  discontinued!: boolean;

  @Expose()
  isActive!: boolean;

  @Expose()
  categoryID!: string | null;

  @Expose({ name: 'categoryName' })
  kategori_ad!: string | null;
}

export interface CreateProductDto {
  name: string;
  unitPrice: number;
  unitInStock: number;
  discontinued: boolean;
  categoryID?: string | null;
}

export interface UpdateProductDto {
  name: string;
  unitPrice: number;
  unitInStock: number;
  discontinued: boolean;
  categoryID?: string | null;
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

  getAllWithCategory: async (): Promise<ApiResponse<Product[]>> => {
    const result = await apiClient.get<unknown[]>('/Product/with-category');
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const products = plainToInstance(Product, result.data);
    return { data: products, error: null };
  },

  getById: async (id: string): Promise<ApiResponse<Product>> => {
    const result = await apiClient.get<unknown>(`/Product/${id}`);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const product = plainToInstance(Product, result.data);
    return { data: product, error: null };
  },

  create: async (dto: CreateProductDto): Promise<ApiResponse<Product>> => {
    const result = await apiClient.post<unknown>('/Product', dto);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const product = plainToInstance(Product, result.data);
    return { data: product, error: null };
  },

  update: async (id: string, dto: UpdateProductDto): Promise<ApiResponse<void>> => {
    return apiClient.put<void>(`/Product/${id}`, dto);
  },

  delete: async (id: string): Promise<ApiResponse<void>> => {
    return apiClient.delete<void>(`/Product/${id}`);
  },
};
