import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
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

export const createDiscount = ({ data }: { data: CreateDiscountInput & { businessId: number } }): Promise<Discount> => {
  return api.post('/v1/discounts', data);
};

type UseCreateDiscountOptions = {
  mutationConfig?: MutationConfig<typeof createDiscount>;
};

export const useCreateDiscount = ({ mutationConfig }: UseCreateDiscountOptions = {}) => {
  const queryClient = useQueryClient();
  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({ queryKey: ['discounts'] });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateDiscountInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createDiscount({ data: { ...data, businessId } });
    },
  });
};
