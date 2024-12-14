import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Product } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createProductInputSchema = z.object({
  name: z.string().min(1, 'Name is required.'),
  stock: z.coerce.number().int('Stock must be an integer.').min(0, 'Stock must not be negative.'),
  price: z.coerce.number().min(0, 'Price must not be negative.'),
  taxIds: z.array(z.number().int()),
  modifierIds: z.array(z.number().int()),
});

export type CreateProductInput = z.infer<typeof createProductInputSchema>;

export const createProduct = ({ data }: { data: CreateProductInput & { businessId: number }}): Promise<Product> => {
  return api.post('/v1/products',  data);
};

type UseCreateProductOptions = {
  mutationConfig?: MutationConfig<typeof createProduct>;
};

export const useCreateProduct = ({ mutationConfig }: UseCreateProductOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['products'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateProductInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createProduct({ data: { ...data, businessId } });
    },
  });
};
