export type EntityBase = {
  id: number;
  createdAt: Date;
  modifiedAt: Date;
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
  price: number;
  stock: number;
}>;

export type Tax = Entity<{ name: string; rate: number }>;
