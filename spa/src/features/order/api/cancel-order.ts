import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const cancelOrder = ({ orderId }: { orderId: number }): Promise<Order> => {
  return api.post(`/v1/orders/${orderId}/cancel`);
};

type UseCancelOrderOptions = {
  mutationConfig?: MutationConfig<typeof cancelOrder>;
};

export const useCancelOrder = ({ mutationConfig }: UseCancelOrderOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.refetchQueries({
        queryKey: ['order', args[1].orderId],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: cancelOrder,
  });
};
