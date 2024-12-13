import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Tax } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createTaxInputSchema = z.object({
  name: z.string(),
  rate: z.coerce.number(),
});

export type CreateTaxInput = z.infer<typeof createTaxInputSchema>;

export const createTax = ({ data }: { data: CreateTaxInput & { businessId: number }}): Promise<Tax> => {
  return api.post('/v1/taxes', data);
};

type UseCreateTaxOptions = {
  mutationConfig?: MutationConfig<typeof createTax>;
};

export const useCreateTax = ({ mutationConfig }: UseCreateTaxOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['taxes'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateTaxInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createTax({ data: { ...data, businessId } });
    },
  });
};
