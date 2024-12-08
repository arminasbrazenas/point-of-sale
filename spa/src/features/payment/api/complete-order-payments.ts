import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';

type CompleteOrderPaymentsData = {
  orderId: number;
};

export const completeOrderPayments = ({ data }: { data: CompleteOrderPaymentsData }): Promise<ServiceCharge> => {
  return api.post('/v1/payments/complete', data);
};

type UseCompleteOrderPaymentsOptions = {
  mutationConfig?: MutationConfig<typeof completeOrderPayments>;
};

export const useCompleteOrderPayments = ({ mutationConfig }: UseCompleteOrderPaymentsOptions = {}) => {
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
    mutationFn: completeOrderPayments,
  });
};
