import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import {  showNotification }from'@/lib/notifications';
import {useMutation} from '@tanstack/react-query';
import { z } from 'zod';

export const registerApplicationUserSchema = z.object({
    firstName: z.string().min(1, 'First name cannot be empty'),
    lastName: z.string().min(1, 'Last name cannot be empty'),
    phoneNumber: z.string().min(1, 'Phone number cannot be empty'),
    email: z.coerce.string().min(1, 'Email cannot be empty'),
    password: z.string().min(8, 'Password must be at least 8 characters length'),
    role: z.literal('BusinessOwner').default('BusinessOwner'),
  });

export type RegisterApplicationUserInput = z.infer<typeof registerApplicationUserSchema>;

export const registerApplicationUser = ({ data }: { data: RegisterApplicationUserInput }): Promise<ApplicationUser> => {
    return api.post('/v1/users/register', data);
};

type UseRegisterApplicationUserOptions = {
  mutationConfig?: MutationConfig<typeof registerApplicationUser>;
};

export const useRegisterApplicationUser = ({ mutationConfig }: UseRegisterApplicationUserOptions = {}) => {

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    mutationFn: registerApplicationUser,
    onSuccess: (data, ...args) => {
      showNotification({
                type: 'success',
                title: 'Business owner registered successfully.',
              });
        onSuccess?.(data, ...args);
    },
    ...restConfig,
});
};
