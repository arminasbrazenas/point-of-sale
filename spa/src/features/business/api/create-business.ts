import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Business } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createBusinessInputSchema = z.object({
  name: z.string(),
  address: z.string(),
  email: z.string(),
  phoneNumber: z.string(),
  businessOwnerId: z.number()
});

export type CreateBusinessInput = z.infer<typeof createBusinessInputSchema>;

export const createBusiness = ({ data }: { data: CreateBusinessInput}): Promise<Business> => {
    return api.post('/v1/businesses', data);
};

type UseCreateBusinessOptions = {
  mutationConfig?: MutationConfig<typeof createBusiness>;
};

export const useCreateBusiness = ({ mutationConfig }: UseCreateBusinessOptions = {}) => {
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
    mutationFn: async ({ data }: { data: CreateBusinessInput }) => {
      return createBusiness({ data});
    },
  });
};