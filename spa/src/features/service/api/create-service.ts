import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Service } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createServiceInputSchema = z.object({
  name: z.string(),
  price: z.coerce.number(),
  durationInMinutes: z.coerce.number(),
  providedByEmployeesWithId: z.array(z.number()),
});

export type CreateServiceInput = z.infer<typeof createServiceInputSchema>;

export const createService = ({ data }: { data: CreateServiceInput & { businessId: number } }): Promise<Service> => {
  return api.post('/v1/services', data);
};

type UseCreateServiceOptions = {
  mutationConfig?: MutationConfig<typeof createService>;
};

export const useCreateService = ({ mutationConfig }: UseCreateServiceOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['services'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateServiceInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createService({ data: { ...data, businessId } });
    },
  });
};
