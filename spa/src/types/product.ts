import { Entity } from './shared';

export type Product = Entity<{
  name: string;
  price: number;
  stock: number;
}>;

export type CreateProductDTO = {
  name: string;
  price: number;
  stock: number;
  taxIds: number[];
};
