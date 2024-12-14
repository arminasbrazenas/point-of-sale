import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';

type CompleteOrderData = {
  orderId: number;
};

export const completeOrder = ({ data }: { data: CompleteOrderData }): Promise<ServiceCharge> => {
  return api.post(`/v1/orders/${data.orderId}/complete`);
};

type UseCompleteOrderOptions = {
  mutationConfig?: MutationConfig<typeof completeOrder>;
};

export const useCompleteOrder = ({ mutationConfig }: UseCompleteOrderOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['order'],
      });
      queryClient.invalidateQueries({
        queryKey: ['orders'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: completeOrder,
  });
};
