import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteDiscount = ({ discountId }: { discountId: number }): Promise<void> => {
  return api.delete(`/v1/discounts/${discountId}`);
};

type UseDeleteDiscountOptions = {
  mutationConfig?: MutationConfig<typeof deleteDiscount>;
};

export const useDeleteDiscount = ({ mutationConfig }: UseDeleteDiscountOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['discounts'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteDiscount,
  });
};
