import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteServiceCharge = ({ serviceChargeId }: { serviceChargeId: number }): Promise<void> => {
  return api.delete(`/v1/service-charges/${serviceChargeId}`);
};

type UseDeleteServiceChargeOptions = {
  mutationConfig?: MutationConfig<typeof deleteServiceCharge>;
};

export const useDeleteServiceCharge = ({ mutationConfig }: UseDeleteServiceChargeOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['service-charges'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteServiceCharge,
  });
};
