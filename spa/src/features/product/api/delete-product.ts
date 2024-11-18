import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteProduct = ({ productId }: { productId: number }): Promise<void> => {
  return api.delete(`/v1/products/${productId}`);
};

type UseDeleteProductOptions = {
  mutationConfig?: MutationConfig<typeof deleteProduct>;
};

export const useDeleteProduct = ({ mutationConfig }: UseDeleteProductOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['products'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteProduct,
  });
};
