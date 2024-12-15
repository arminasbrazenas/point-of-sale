import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteBusiness = ({ businessId }: { businessId: number }): Promise<void> => {
  return api.delete(`/v1/businesses/${businessId}`);
};

type UseDeleteBusinessOptions = {
  mutationConfig?: MutationConfig<typeof deleteBusiness>;
};

export const useDeleteBusiness = ({ mutationConfig }: UseDeleteBusinessOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['businesses'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteBusiness,
  });
};
