import { logoutApplicationUser } from '@/features/application-user/api/logout-application-user';
import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
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
  const { updateApplicationUser } = useAppStore.getState();
  const role = useAppStore((state) => state.applicationUser?.role || null);

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      if (role=='BusinessOwner')
        {
          updateApplicationUser({'businessId':null})
      }
      queryClient.invalidateQueries({
        queryKey: ['businesses'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteBusiness,
  });
};
