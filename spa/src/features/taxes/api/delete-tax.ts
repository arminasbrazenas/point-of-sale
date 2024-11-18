import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteTax = ({ taxId }: { taxId: number }): Promise<void> => {
  return api.delete(`/v1/taxes/${taxId}`);
};

type UseDeleteTaxOptions = {
  mutationConfig?: MutationConfig<typeof deleteTax>;
};

export const useDeleteTax = ({ mutationConfig }: UseDeleteTaxOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['taxes'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteTax,
  });
};
