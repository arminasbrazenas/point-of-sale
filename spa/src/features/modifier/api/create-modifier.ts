import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createModifierInputSchema = z.object({
  name: z.string(),
  stock: z.coerce.number(),
  price: z.coerce.number(),
});

export type CreateModifierInput = z.infer<typeof createModifierInputSchema>;

export const createModifier = ({ data }: { data: CreateModifierInput & { businessId: number }}): Promise<Modifier> => {
  return api.post('/v1/modifiers', data);
};

type UseCreateModifierOptions = {
  mutationConfig?: MutationConfig<typeof createModifier>;
};

export const useCreateModifier = ({ mutationConfig }: UseCreateModifierOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  
  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['modifiers'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateModifierInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createModifier({ data: { ...data, businessId } });
    },
  });
};
