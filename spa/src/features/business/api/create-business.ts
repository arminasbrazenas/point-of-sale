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
  businessOwnerId: z.number(),
  startHour: z.number().min(0).max(23),
  startMinute: z.number().min(0).max(59),
  endMinute: z.number().min(0).max(59),
  endHour: z.number().min(0).max(23),
});

export type CreateBusinessInput = z.infer<typeof createBusinessInputSchema>;

export const createBusiness = ({ data }: { data: CreateBusinessInput }): Promise<Business> => {
  return api.post('/v1/businesses', data);
};

type UseCreateBusinessOptions = {
  mutationConfig?: MutationConfig<typeof createBusiness>;
};

export const useCreateBusiness = ({ mutationConfig }: UseCreateBusinessOptions = {}) => {
  const queryClient = useQueryClient();
  const { updateApplicationUser } = useAppStore.getState();
  const role = useAppStore((state) => state.applicationUser?.role || null);

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (data, ...args) => {
      if (role == 'BusinessOwner')
        updateApplicationUser({ "businessId": data.id })
      queryClient.invalidateQueries({
        queryKey: ['businesses'],
      });
      queryClient.invalidateQueries({ queryKey: ['business'] });
      onSuccess?.(data, ...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateBusinessInput }) => {
      return createBusiness({ data });
    },
  });
};