import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Product } from '@/types/product';
import { useMutation } from '@tanstack/react-query';
import { z } from 'zod';

export const createProductInputSchema = z.object({
  name: z.string().min(1, 'Name is required.'),
  stock: z.number().int('Stock must be an integer.').min(0, 'Stock must not be negative.'),
  price: z.number().min(0, 'Price must not be negative.'),
  taxIds: z.array(z.number().int()),
});

export type CreateProductInput = z.infer<typeof createProductInputSchema>;

export const createProduct = ({ data }: { data: CreateProductInput }): Promise<Product> => {
  return api.post('/v1/products', data);
};

type UseCreateProductOptions = {
  mutationConfig?: MutationConfig<typeof createProduct>;
};

export const useCreateProduct = ({ mutationConfig }: UseCreateProductOptions = {}) => {
  return useMutation({ ...mutationConfig, mutationFn: createProduct });
};
