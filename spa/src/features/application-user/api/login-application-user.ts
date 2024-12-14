import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import {useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const loginApplicationUserSchema = z.object({
    email: z.coerce.string(),
    password: z.string(),
  });

export type LoginApplicationUserInput = z.infer<typeof loginApplicationUserSchema>;

export const loginApplicationUser = ({ data }: { data: LoginApplicationUserInput }): Promise<ApplicationUser> => {
  return api.post('/v1/users/login', data);
};

type UseLoginApplicationUserOptions = {
  mutationConfig?: MutationConfig<typeof loginApplicationUser>;
};

export const useLoginApplicationUser = ({ mutationConfig }: UseLoginApplicationUserOptions = {}) => {
  const queryClient = useQueryClient();
  const setApplicationUser = useAppStore((state) => state.setApplicationUser);

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    mutationFn: loginApplicationUser,
    onSuccess: (data, ...args) => {
        setApplicationUser({
            id: data.id,
            businessId: data.businessId,
            role: data.role,
        });

        onSuccess?.(data, ...args);

        queryClient.invalidateQueries({
            queryKey: ['applicationUsers'],
        });
    },
    ...restConfig,
});
};