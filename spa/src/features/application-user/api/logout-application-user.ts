import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import {useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';


export const logoutApplicationUser = (): Promise<void> => {
  return api.post('/v1/users/logout');
};

type UseLogoutApplicationUserOptions = {
  mutationConfig?: MutationConfig<typeof logoutApplicationUser>;
};

export const useLoginApplicationUser = ({ mutationConfig }: UseLogoutApplicationUserOptions = {}) => {
  const queryClient = useQueryClient();
  const setApplicationUser = useAppStore((state) => state.setApplicationUser);

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    mutationFn: logoutApplicationUser,
    onSuccess: (data, ...args) => {
        setApplicationUser(
            null
        );

        onSuccess?.(data, ...args);

        queryClient.invalidateQueries({
            queryKey: ['applicationUsers'],
        });
    },
    ...restConfig,
});
};