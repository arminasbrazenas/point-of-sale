import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const confirmPaymentIntent = ({ paymentIntentId }: { paymentIntentId: string }): Promise<void> => {
  return api.post(`/v1/payments/online/intents/${paymentIntentId}/confirm`);
};

type UseConfirmPaymentIntentOptions = {
  mutationConfig?: MutationConfig<typeof confirmPaymentIntent>;
};

export const useConfirmPaymentIntent = ({ mutationConfig }: UseConfirmPaymentIntentOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['payments'],
      });
      queryClient.invalidateQueries({
        queryKey: ['order-tips'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: confirmPaymentIntent,
  });
};
