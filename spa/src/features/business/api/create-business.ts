import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Business } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createBusinessInputSchema = z.object({
  name: z.string(),
  address: z.string(),
  email: z.string(),
  phoneNumber: z.string(),
  password: z.string(),
});

export type CreateBusinessInput = z.infer<typeof createBusinessInputSchema>;

export const createBusiness = ({ data }: { data: CreateBusinessInput & { businessOwnerId: number }}): Promise<Business> => {
    return api.post('/v1/businesses', data);
};

type UseCreateBusinessOptions = {
  mutationConfig?: MutationConfig<typeof createBusiness>;
};

export const useCreateBusiness = ({ mutationConfig }: UseCreateBusinessOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessOwnerId = useAppStore((state) => state.applicationUser?.id);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['businesses'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateBusinessInput }) => {
      if (!businessOwnerId) {
        const error = new Error('Unauthorized');
        (error as any).statusCode = 401;
        throw error;
      }

      return createBusiness({ data: { ...data, businessOwnerId } });
    },
  });
};