import 'reflect-metadata';
import { Expose, Type, plainToInstance } from 'class-transformer';
import { apiClient, type ApiResponse } from './apiClient';

export class OrderProduct {
  @Expose({ name: 'id' })
  id!: string;

  @Expose({ name: 'name' })
  name!: string;

  @Expose()
  unitPrice!: number;

  @Expose()
  unitInStock!: number;
}

export class Order {
  @Expose({ name: 'id' })
  id!: string;

  @Expose({ name: 'orderDate' })
  orderDate!: string;

  @Expose()
  totalAmount!: number;

  @Expose()
  customerId!: string;

  @Expose()
  customerName!: string;

  @Expose({ name: 'products' })
  @Type(() => OrderProduct)
  products!: OrderProduct[];
}

export interface CreateOrderDto {
  orderDate: string;
  totalAmount: number;
  customerId: string;
  productIds: string[];
}

export interface UpdateOrderDto {
  orderDate: string;
  totalAmount: number;
  customerId: string;
  productIds: string[];
}

const mapOrders = (data: unknown): Order[] =>
  plainToInstance(Order, data as object[]);

const mapOrder = (data: unknown): Order =>
  plainToInstance(Order, data as object);

export const orderService = {
  getAll: async (): Promise<ApiResponse<Order[]>> => {
    const result = await apiClient.get<unknown[]>('/Order');
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const orders = mapOrders(result.data);
    // Map nested product array into OrderProduct instances
    orders.forEach((o) => {
      if (o.products) {
        o.products = plainToInstance(OrderProduct, o.products as unknown as object[]);
      }
    });
    return { data: orders, error: null };
  },

  getById: async (id: string): Promise<ApiResponse<Order>> => {
    const result = await apiClient.get<unknown>(`/Order/${id}`);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const order = mapOrder(result.data);
    if (order.products) {
      order.products = plainToInstance(OrderProduct, order.products as unknown as object[]);
    }
    return { data: order, error: null };
  },

  create: async (dto: CreateOrderDto): Promise<ApiResponse<Order>> => {
    const result = await apiClient.post<unknown>('/Order', dto);
    if (result.error !== null) {
      return { data: null, error: result.error };
    }
    const order = mapOrder(result.data);
    if (order.products) {
      order.products = plainToInstance(OrderProduct, order.products as unknown as object[]);
    }
    return { data: order, error: null };
  },

  update: async (id: string, dto: UpdateOrderDto): Promise<ApiResponse<void>> => {
    return apiClient.put<void>(`/Order/${id}`, dto);
  },

  delete: async (id: string): Promise<ApiResponse<void>> => {
    return apiClient.delete<void>(`/Order/${id}`);
  },
};