import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createServiceChargeInputSchema = z.object({
  name: z.string(),
  amount: z.coerce.number(),
  pricingStrategy: z.coerce.string(),
});

export type CreateServiceChargeInput = z.infer<typeof createServiceChargeInputSchema>;

export const createServiceCharge = ({ data }: { data: CreateServiceChargeInput & { businessId: number } }): Promise<ServiceCharge> => {
  return api.post('/v1/service-charges', data);
};

type UseCreateServiceChargeOptions = {
  mutationConfig?: MutationConfig<typeof createServiceCharge>;
};

export const useCreateServiceCharge = ({ mutationConfig }: UseCreateServiceChargeOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['service-charges'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateServiceChargeInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createServiceCharge({ data: { ...data, businessId } });
    },
  });
};
