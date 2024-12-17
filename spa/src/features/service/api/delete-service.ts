import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteService = ({ serviceId }: { serviceId: number }): Promise<void> => {
  return api.delete(`/v1/services/${serviceId}`);
};

type UseDeleteServiceOptions = {
  mutationConfig?: MutationConfig<typeof deleteService>;
};

export const useDeleteService = ({ mutationConfig }: UseDeleteServiceOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['services'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteService,
  });
};
