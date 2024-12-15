import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteEmployee = ({ employeeId }: { employeeId: number }): Promise<void> => {
  return api.delete(`/v1/users/${employeeId}`);
};

type UseDeleteEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof deleteEmployee>;
};

export const useDeleteEmployee = ({ mutationConfig }: UseDeleteEmployeeOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['employees'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteEmployee,
  });
};
