import 'reflect-metadata';
import { Expose, plainToInstance } from 'class-transformer';
import { apiClient, type ApiResponse } from './apiClient';
import { Product } from './product.service';

export class Category {
  @Expose()
  id!: string;

  @Expose()
  name!: string;

  @Expose()
  description!: string;

  @Expose()
  isActive!: boolean;

  @Expose()
  createdDate!: string;

  @Expose()
  updatedDate!: string | null;
}

export interface CreateCategoryDto {
  name: string;
  description: string;
  isActive: boolean;
}

export interface UpdateCategoryDto {
  name: string;
  description: string;
  isActive: boolean;
}

export const categoryService = {
  getAll: async (): Promise<ApiResponse<Category[]>> => {
    const result = await apiClient.get<unknown[]>('/Category');
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const categories = plainToInstance(Category, result.data);
    return { data: categories, error: null };
  },

  getById: async (id: string): Promise<ApiResponse<Category>> => {
    const result = await apiClient.get<unknown>(`/Category/${id}`);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const category = plainToInstance(Category, result.data);
    return { data: category, error: null };
  },

  getProducts: async (id: string): Promise<ApiResponse<Product[]>> => {
    const result = await apiClient.get<unknown[]>(`/Category/${id}/products`);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const products = plainToInstance(Product, result.data);
    return { data: products, error: null };
  },

  create: async (dto: CreateCategoryDto): Promise<ApiResponse<Category>> => {
    const result = await apiClient.post<unknown>('/Category', dto);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const category = plainToInstance(Category, result.data);
    return { data: category, error: null };
  },

  update: async (id: string, dto: UpdateCategoryDto): Promise<ApiResponse<void>> => {
    return apiClient.put<void>(`/Category/${id}`, dto);
  },

  delete: async (id: string): Promise<ApiResponse<void>> => {
    return apiClient.delete<void>(`/Category/${id}`);
  },
};
