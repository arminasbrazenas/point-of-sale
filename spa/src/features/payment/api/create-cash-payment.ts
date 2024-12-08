import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createPaymentInputSchema = z.object({
  orderId: z.coerce.number(),
  paymentAmount: z.coerce.number(),
});

export type CreatePaymentInput = z.infer<typeof createPaymentInputSchema>;

export const createCashPayment = ({ data }: { data: CreatePaymentInput }): Promise<ServiceCharge> => {
  return api.post('/v1/payments/cash', data);
};

type UseCreateCashPaymentOptions = {
  mutationConfig?: MutationConfig<typeof createCashPayment>;
};

export const useCreateCashPayment = ({ mutationConfig }: UseCreateCashPaymentOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['payments'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createCashPayment,
  });
};
