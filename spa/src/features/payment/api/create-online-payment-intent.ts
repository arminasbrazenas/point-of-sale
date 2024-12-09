import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { PaymentIntent } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import z from 'zod';

export const createOnlinePaymentIntentSchema = z.object({
  orderId: z.coerce.number(),
  paymentAmount: z.coerce.number(),
});

export type CreateOnlinePaymentIntentInput = z.infer<typeof createOnlinePaymentIntentSchema>;

export const createOnlinePaymentIntent = ({
  data,
}: {
  data: CreateOnlinePaymentIntentInput;
}): Promise<PaymentIntent> => {
  return api.post('/v1/payments/online/intents', data);
};

type UseCreateOnlinePaymentIntentOptions = {
  mutationConfig?: MutationConfig<typeof createOnlinePaymentIntent>;
};

export const useCreateOnlinePaymentIntent = ({ mutationConfig }: UseCreateOnlinePaymentIntentOptions = {}) => {
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
    mutationFn: createOnlinePaymentIntent,
  });
};
