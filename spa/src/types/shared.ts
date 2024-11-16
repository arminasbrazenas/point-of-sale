export type EntityBase = {
  id: number;
  createdAt: Date;
  modifiedAt: Date;
};

export type Entity<T> = {
  [K in keyof T]: T[K];
} & EntityBase;

export type PagedResponse<T> = {
  page: number;
  itemsPerPage: number;
  items: T;
};

export type ErrorResponse = {
  errorMessage: string;
};
