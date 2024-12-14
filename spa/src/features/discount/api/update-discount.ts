import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getDiscountQueryOptions } from './get-discount';
import { Discount } from '@/types/api';

export const updateDiscountInputSchema = z.object({
  amount: z.coerce.number().optional(),
  pricingStrategy: z.string().optional(),
  validUntil: z.date().optional(),
  appliesToProductIds: z.array(z.number()).optional(),
});

export type UpdateDiscountInput = z.infer<typeof updateDiscountInputSchema>;

export const updateDiscount = ({
  data,
  discountId,
}: {
  data: UpdateDiscountInput;
  discountId: number;
}): Promise<Discount> => {
  return api.patch(`/v1/discounts/${discountId}`, data);
};

type UseUpdateDiscountOptions = {
  mutationConfig?: MutationConfig<typeof updateDiscount>;
};

export const useUpdateDiscount = ({ mutationConfig }: UseUpdateDiscountOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (discount, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['discounts'],
      });
      queryClient.setQueryData(getDiscountQueryOptions(discount.id).queryKey, () => discount);
      onSuccess?.(discount, ...args);
    },
    ...restConfig,
    mutationFn: updateDiscount,
  });
};
