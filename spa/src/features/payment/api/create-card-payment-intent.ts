import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { PaymentIntent } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import z from 'zod';

export const createCardPaymentIntentSchema = z.object({
  orderId: z.coerce.number(),
  paymentAmount: z.coerce.number(),
  tipAmount: z.coerce.number(),
  businessId: z.coerce.number(),
  employeeId: z.coerce.number(),
});

export type CreateCardPaymentIntentInput = z.infer<typeof createCardPaymentIntentSchema>;

export const createCardPaymentIntent = ({ data }: { data: CreateCardPaymentIntentInput }): Promise<PaymentIntent> => {
  return api.post('/v1/payments/card/intents', data);
};

type UseCreateCardPaymentIntentOptions = {
  mutationConfig?: MutationConfig<typeof createCardPaymentIntent>;
};

export const useCreateCardPaymentIntent = ({ mutationConfig }: UseCreateCardPaymentIntentOptions = {}) => {
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
    mutationFn: createCardPaymentIntent,
  });
};
