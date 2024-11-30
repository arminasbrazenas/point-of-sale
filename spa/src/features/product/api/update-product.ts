import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Product } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getProductQueryOptions } from './get-product';

export const updateProductInputSchema = z.object({
  name: z.string().optional(),
  stock: z.coerce.number().optional(),
  price: z.coerce.number().optional(),
  taxIds: z.array(z.number().int()).optional(),
  modifierIds: z.array(z.number().int()).optional(),
});

export type UpdateProductInput = z.infer<typeof updateProductInputSchema>;

export const updateProduct = ({
  data,
  productId,
}: {
  data: UpdateProductInput;
  productId: number;
}): Promise<Product> => {
  return api.patch(`/v1/products/${productId}`, data);
};

type UseUpdateProductOptions = {
  mutationConfig?: MutationConfig<typeof updateProduct>;
};

export const useUpdateProduct = ({ mutationConfig }: UseUpdateProductOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (product, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['products'],
      });
      queryClient.setQueryData(getProductQueryOptions(product.id).queryKey, () => product);
      onSuccess?.(product, ...args);
    },
    ...restConfig,
    mutationFn: updateProduct,
  });
};
