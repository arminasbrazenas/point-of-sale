import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import {useMutation } from '@tanstack/react-query';


export const logoutApplicationUser = (): Promise<void> => {
  return api.post('/v1/users/logout');
};

type UseLogoutApplicationUserOptions = {
  mutationConfig?: MutationConfig<typeof logoutApplicationUser>;
};

export const useLogoutApplicationUser = ({ mutationConfig }: UseLogoutApplicationUserOptions = {}) => {
  const resetApplicationUser = useAppStore((state) => state.resetApplicationUser);
  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    mutationFn: logoutApplicationUser,
    onSuccess: (data, ...args) => {
      resetApplicationUser();
        onSuccess?.(data, ...args);
    },
    ...restConfig,
});
};