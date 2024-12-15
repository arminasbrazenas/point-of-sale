import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createEmployeeInputSchema = z.object({
  firstName: z.string(),
  lastName: z.string(),
  email: z.string(),
  phoneNumber: z.string(),
  password: z.string(),
});

export type CreateEmployeeInput = z.infer<typeof createEmployeeInputSchema>;

export const createEmployee = ({ data }: { data: CreateEmployeeInput & { businessId: number }}): Promise<ApplicationUser> => {
    return api.post('/v1/users/register', { ...data, role: 'Employee' });
};

type UseCreateEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof createEmployee>;
};

export const useCreateEmployee = ({ mutationConfig }: UseCreateEmployeeOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['employees'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateEmployeeInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createEmployee({ data: { ...data, businessId } });
    },
  });
};
