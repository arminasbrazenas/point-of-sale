import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

type RefundOrderPaymentsData = {
  orderId: number;
};

export const refundOrderPayments = ({ data }: { data: RefundOrderPaymentsData }): Promise<void> => {
  return api.post('/v1/payments/refund', data);
};

type UseRefundOrderPaymentsOptions = {
  mutationConfig?: MutationConfig<typeof refundOrderPayments>;
};

export const useRefundOrderPayments = ({ mutationConfig }: UseRefundOrderPaymentsOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['order'],
      });
      queryClient.invalidateQueries({
        queryKey: ['payments'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: refundOrderPayments,
  });
};
