import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Discount } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createDiscountInputSchema = z.object({
  amount: z.coerce.number(),
  pricingStrategy: z.string(),
  validUntil: z.date(),
  appliesToProductIds: z.array(z.number()),
});

export type CreateDiscountInput = z.infer<typeof createDiscountInputSchema>;

export const createDiscount = ({ data }: { data: CreateDiscountInput }): Promise<Discount> => {
  return api.post('/v1/discounts', data);
};

type UseCreateDiscountOptions = {
  mutationConfig?: MutationConfig<typeof createDiscount>;
};

export const useCreateDiscount = ({ mutationConfig }: UseCreateDiscountOptions = {}) => {
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
    mutationFn: createDiscount,
  });
};
