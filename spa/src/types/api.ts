export enum OrderStatus {
  Open = 'Open',
  Closed = 'Closed',
  Canceled = 'Canceled',
  Refunded = 'Refunded',
}

export type EntityBase = {
  id: number;
  createdAt: string;
  modifiedAt: string;
};

export type Entity<T> = {
  [K in keyof T]: T[K];
} & EntityBase;

export type PaginationFilter = {
  page: number;
  itemsPerPage: number;
};

export type PagedResponse<T> = {
  page: number;
  itemsPerPage: number;
  totalItems: number;
  items: T[];
};

export type ErrorResponse = {
  errorMessage: string;
};

export type Product = Entity<{
  name: string;
  priceWithoutTaxes: number;
  priceWithTaxes: number;
  stock: number;
  taxes: Tax[];
  modifiers: Modifier[];
}>;

export type Tax = Entity<{ name: string; rate: number }>;

export type OrderItemModifier = Entity<{ modifierId?: number; name: string; price: number }>;

export type OrderItem = Entity<{
  productId?: number;
  name: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  modifiers: OrderItemModifier[];
}>;

export type Order = Entity<{
  orderItems: OrderItem[];
  totalPrice: number;
  status: OrderStatus;
}>;

export type OrderReceipt = {
  totalPrice: number;
  orderItems: OrderItem[];
  taxTotal: number;
};

export type Modifier = Entity<{ name: string; price: number; stock: number }>;
